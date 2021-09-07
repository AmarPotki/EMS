import { Component, ViewChild, AfterViewInit, Output, EventEmitter, OnInit, OnDestroy, Input } from '@angular/core';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';
import { MatDialog, MatSnackBar } from '@angular/material';
import { TreeService } from 'app/shared/services/tree.service';
import { ConfigurationService } from 'app/shared/services/configuration.service';
import { DeleteDialogComponent } from 'app/shared/components/delete-dialog/delete-dialog.component';
 import { Subscription } from 'rxjs';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.scss']
})
export class LocationListComponent implements OnInit, OnDestroy {
  @Input('treeType') treeType: string;

  nodeName = '';
  selectedLocation: any;
  public data: any[] = [];
  @ViewChild('myTree') public myTree: jqxTreeComponent;

  public source = {
    datatype: 'json',
    datafields: [
      { name: 'id' },
      { name: 'parentId' },
      { name: 'name' },
      { name: 'value' },
      { name: 'hasChildren' }
    ],
    id: 'id',
    localdata: this.data
  };
  public dataAdapter = new jqx.dataAdapter(this.source, { autoBind: true });
  public records: any =
    this.dataAdapter.getRecordsHierarchy('id', 'parentId', 'hasChildren', 'items', [{ name: 'name', map: 'label' }]);
  private getEmittedAddItemValueRef: Subscription = null;
  private getEmittedUpdateItemValueRef: Subscription = null;
  private getEmittedRemoveItemValueRef: Subscription = null;

  constructor(
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    private treeService: TreeService,
    private configurationService: ConfigurationService,
    private eventHandlerService: EventHandlerService
  ) {
    this.configurationService.fetch();
    this.getAllNodes();
    console.log("Type", this.treeType);
  }

  ngOnInit(): void {
    this.handleAddItemObservable();
    this.handleUpdateItemObservable();
    this.handleRemoveItemObservable();
  }

  ngOnDestroy(): void {
    if (this.getEmittedAddItemValueRef) { this.getEmittedAddItemValueRef.unsubscribe(); }
    if (this.getEmittedAddItemValueRef) { this.getEmittedUpdateItemValueRef.unsubscribe(); }
    if (this.getEmittedAddItemValueRef) { this.getEmittedRemoveItemValueRef.unsubscribe(); }
  }

  handleAddItemObservable(): void {
    this.getEmittedAddItemValueRef =
      this.eventHandlerService.getEmittedAddLocationValue$().subscribe((emittedName: string) => {
        if (emittedName.trim()) {
          this.nodeName = emittedName;
          this.AddNode();
        }
      });
  }

  handleUpdateItemObservable(): void {
    this.getEmittedUpdateItemValueRef =
      this.eventHandlerService.getEmittedUpdateLocationValue$().subscribe((emittedName: string) => {
        if (emittedName.trim()) {
          this.nodeName = emittedName;
          this.UpdateNode();
        }
      });
  }

  handleRemoveItemObservable(): void {
    this.getEmittedRemoveItemValueRef =
      this.eventHandlerService.getEmittedRemoveLocationValue$().
        subscribe((eventEmitted: boolean) => { if (eventEmitted) { this.RemoveNode() } });
  }

  handleOnSelectItem(): void {
    this.selectedLocation = this.myTree.getSelectedItem();
    this.eventHandlerService.emitCurrentSelectedLocationNodeEvent(this.selectedLocation);
  }

  getAllNodes(): void {
    let url = this.configurationService.getAllLocationsUrl;
    this.treeService.getAllNodes(url).subscribe((items: any[]) => {
      items.forEach(item => {
        item.id = item.id.toString();
        item.parentId = item.parentId.toString();
        item.hasChildren = item.hasChildren.toString();
      });
      this.source.localdata = items;
      this.dataAdapter = new jqx.dataAdapter(this.source, { autoBind: true });
      this.records = this.dataAdapter.getRecordsHierarchy('id', 'parentId', 'items', [{ name: 'name', map: 'label' }]);
    })
  }

  getNodeChilds(parentId: number): void {
    let url = this.configurationService.getLocationsUrl;
    this.treeService.getNodeChild(url, parentId).subscribe((items: any[]) => {
      items.forEach(item => {
        item.id = item.id.toString();
        item.parentId = item.parentId.toString();
      });
      this.source.localdata.push(...items);
      this.dataAdapter = new jqx.dataAdapter(this.source, { autoBind: true });
      this.records = this.dataAdapter.getRecordsHierarchy('id', 'parentId', 'items', [{ name: 'name', map: 'label' }]);
    })
  }

  AddNode(): void {
    let parentId: number;
    if (this.selectedLocation == null) { parentId = null; }
    else { parentId = parseInt(this.selectedLocation['id']); }
    let node = { name: this.nodeName, parentId: parentId };
    if (this.nodeName.trim()) {
      this.treeService.AddNode(this.configurationService.createLocationUrl, node).subscribe(
        _ => {
          if (this.selectedLocation == null) { this.getAllNodes(); }
          else { this.ExpandNode(); }
          this.nodeName = '';
          this.snackBar.open('مکان جدید باموفقیت اضافه شد', 'بستن');
        },
        _ => {
          this.snackBar.open('خطایی در افزودن نود بوجود آمده است', 'بستن');
        }
      );
    }
  };

  UpdateNode(): void {
    let id = parseInt(this.selectedLocation['id']);
    if (!this.nodeName.trim()) { return; }
    let node = { id: id, name: this.nodeName };
    this.treeService.updateNode(this.configurationService.updateLocationUrl, node).subscribe(
      _ => {
        this.selectedLocation['label'] = this.nodeName;
        this.myTree.updateItem({ label: this.nodeName }, this.selectedLocation.element);
        this.myTree.render();
        this.snackBar.open('مکان مورد نظر با موفقیت ویرایش شد', 'بستن');
      },
      _ => {
        this.snackBar.open('خطایی در ویرایش مکان بوجود آمده است', 'بستن');
      }
    );
  }

  ExpandNode(): void {
    let parentId = parseInt(this.selectedLocation['id']);
    if (this.selectedLocation != null) {
      let nodeChildes = this.source.localdata.filter(x => x.parentId == parentId);
      if (nodeChildes.length) { this.source.localdata.filter(x => x.parentId == parentId); }
      this.getNodeChilds(parentId);
      this.myTree.expandItem(this.selectedLocation.element);
    }
  };

  RemoveNode(): void {
    if (this.selectedLocation == null) {
      this.snackBar.open('ابتدا آیتمی را جهت حذف کردن انتخاب کنید', 'بستن');
      return;
    }
    let itemId = parseInt(this.selectedLocation['id']);
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.treeService.deleteNode(this.configurationService.deleteLocationUrl, itemId).subscribe(
          _ => {
            this.myTree.removeItem(this.selectedLocation.element);
            this.myTree.render();
            this.snackBar.open('مکان مورد نظر باموفقیت حذف شد', 'بستن');
          },
          _ => {
            this.snackBar.open('خطایی در حذف مکان بوجود آمده است', 'بستن');
          });
      };
    });
  };
}


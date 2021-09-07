import { Component, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatDialog, MatSnackBar } from '@angular/material';
import { TreeService } from 'app/shared/services/tree.service';
import { ConfigurationService } from 'app/shared/services/configuration.service';
import { DeleteDialogComponent } from 'app/shared/components/delete-dialog/delete-dialog.component';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';
import { EventHandlerService } from 'app/shared/services/event-handler.service';
 
@Component({
  selector: 'app-item-type-list',
  templateUrl: './item-type-list.component.html',
  styleUrls: ['./item-type-list.component.scss']
})
export class ItemTypeListComponent implements OnInit {
  nodeName = '';
  selectedItemType: any;
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
      this.eventHandlerService.getEmittedAddItemTypeValue$().subscribe((emittedName: string) => {
        if (emittedName.trim()) {
          this.nodeName = emittedName;
          this.AddNode();
        }
      });
  }

  handleUpdateItemObservable(): void {
    this.getEmittedUpdateItemValueRef =
      this.eventHandlerService.getEmittedUpdateItemTypeValue$().subscribe((emittedName: string) => {
        if (emittedName.trim()) {
          this.nodeName = emittedName;
          this.UpdateNode();
        }
      });
  }

  handleRemoveItemObservable(): void {
    this.getEmittedRemoveItemValueRef =
      this.eventHandlerService.getEmittedRemoveItemTypeValue$().
        subscribe((eventEmitted: boolean) => { if (eventEmitted) { this.RemoveNode() } });
  }

  handleOnSelectItem(): void {
    this.selectedItemType = this.myTree.getSelectedItem();
    console.log("handleOnSelectItem",this.selectedItemType);
    this.eventHandlerService.emitCurrentSelectedItemTypeNodeEvent(this.selectedItemType);
  }

  getAllNodes(): void {
    let url = this.configurationService.getAllItemTypesUrl;
    console.log("url",url);
    this.treeService.getAllNodes(url).subscribe((items: any[]) => {
      console.log("items",items);
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
    let url = this.configurationService.getItemTypeUrl;
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
    if (this.selectedItemType == null) { parentId = null; }
    else { parentId = parseInt(this.selectedItemType['id']); }
    let node = { name: this.nodeName, parentId: parentId };
    console.log("node",node);
    console.log("url",this.configurationService.createItemTypeUrl);  
    if (this.nodeName.trim()) {
      this.treeService.AddNode(this.configurationService.createItemTypeUrl, node).subscribe(
        _ => {
          if (this.selectedItemType == null) { this.getAllNodes(); }
          else { this.ExpandNode(); }
          this.nodeName = '';
          this.snackBar.open('نوع جدید باموفقیت اضافه شد', 'بستن');
        },
        _ => {
          this.snackBar.open('خطایی در افزودن نود بوجود آمده است', 'بستن');
        }
      );
    }
  };

  UpdateNode(): void {
    let id = parseInt(this.selectedItemType['id']);
    if (!this.nodeName.trim()) { return; }
    let node = { id: id, name: this.nodeName };
    this.treeService.updateNode(this.configurationService.updateItemTypeUrl, node).subscribe(
      _ => {
        this.selectedItemType['label'] = this.nodeName;
        this.myTree.updateItem({ label: this.nodeName }, this.selectedItemType.element);
        this.myTree.render();
        this.snackBar.open('نوع مورد نظر با موفقیت ویرایش شد', 'بستن');
      },
      _ => {
        this.snackBar.open('خطایی در ویرایش نوع بوجود آمده است', 'بستن');
      }
    );
  }

  ExpandNode(): void {
    let parentId = parseInt(this.selectedItemType['id']);
    if (this.selectedItemType != null) {
      let nodeChildes = this.source.localdata.filter(x => x.parentId == parentId);
      if (nodeChildes.length) { this.source.localdata.filter(x => x.parentId == parentId); }
      this.getNodeChilds(parentId);
      this.myTree.expandItem(this.selectedItemType.element);
    }
  };

  RemoveNode(): void {
    if (this.selectedItemType == null) {
      this.snackBar.open('ابتدا آیتمی را جهت حذف کردن انتخاب کنید', 'بستن');
      return;
    }
    let itemId = parseInt(this.selectedItemType['id']);
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.treeService.deleteNode(this.configurationService.deleteItemTypeUrl, itemId).subscribe(
          _ => {
            this.myTree.removeItem(this.selectedItemType.element);
            this.myTree.render();
            this.snackBar.open('نوع مورد نظر باموفقیت حذف شد', 'بستن');
          },
          _ => {
            this.snackBar.open('خطایی در حذف نوع بوجود آمده است', 'بستن');
          });
      };
    });
  };

}

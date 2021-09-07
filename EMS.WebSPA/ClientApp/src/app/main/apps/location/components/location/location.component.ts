import { Component, ViewChild, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatPaginator, MatSort } from '@angular/material';
import { fuseAnimations } from '@fuse/animations';
import { LocationDto } from '../../models/location.model';
 import { Subscription } from 'rxjs';
import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class LocationComponent implements OnDestroy {

  pageType: string;
  productForm: FormGroup;

  dataSource: LocationDto[] | null;
  displayedColumns = ['id', 'name', 'parent', 'buttons'];

  @ViewChild(MatPaginator)
  paginator: MatPaginator;

  @ViewChild(MatSort)
  sort: MatSort;

  showActionsForm: boolean;
  actionFormData: ActionsFormData;
  showActionsButtonSection: boolean;
  private getEmittedCurrentSelectedNodeValueRef: Subscription = null;
  constructor(private eventHandlerService:EventHandlerService) { }
  ngOnDestroy(): void {
    if (this.getEmittedCurrentSelectedNodeValueRef) { this.getEmittedCurrentSelectedNodeValueRef.unsubscribe(); }
  }
  handleShowActionForm($event) {
    this.getEmittedCurrentSelectedNodeValueRef =
      this.eventHandlerService.getEmittedCurrentSelectedLocationNodeValue$().
        subscribe((node: any) => { this.actionFormData = new ActionsFormData($event, node, true); });
  }

  handleCloseActionsForm($event) {
    this.showActionsButtonSection = $event;
  }
}

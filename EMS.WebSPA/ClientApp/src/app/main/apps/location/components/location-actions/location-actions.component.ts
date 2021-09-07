import { Component, Input, OnDestroy } from '@angular/core';
import { TreeActionType } from 'app/shared/models/tree.model';
import { LocationListComponent } from '../location-list/location-list.component';
import { LocationActionsFormComponent } from '../location-actions-form/location-actions-form.component';
  import { Subscription } from 'rxjs';
import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-location-actions',
  templateUrl: './location-actions.component.html',
  styleUrls: ['./location-actions.component.scss'],
  providers: [LocationListComponent, LocationActionsFormComponent]
})
export class LocationActionsComponent implements OnDestroy {
  actionType: TreeActionType;
  selectedItem: any;
  clickedOnButtons = true;
  showActionsButtonSection = false;
  private getEmittedCurrentSelectedNodeValueRef: Subscription = null;
  private getEmittedActionsButtonSectionValueRef: Subscription = null;

  constructor(
    private eventHandlerService: EventHandlerService
  ) {
    this.handleCloseActionForm();
  }

  ngOnDestroy(): void {
    if (this.getEmittedCurrentSelectedNodeValueRef) { this.getEmittedCurrentSelectedNodeValueRef.unsubscribe();}
    if (this.getEmittedActionsButtonSectionValueRef) { this.getEmittedActionsButtonSectionValueRef.unsubscribe();}
  }

  handleCloseActionForm(): void {
    this.getEmittedActionsButtonSectionValueRef =
      this.eventHandlerService.getEmittedShowLocationActionsButtonSectionValue$().
        subscribe((emittedValue: boolean) => {
          if (emittedValue) { this.showActionsButtonSection = true; }
          else if (!emittedValue) { this.showActionsButtonSection = false; }
        });
  }

  preSave(type: string) {
    this.clickedOnButtons = true;
    if (type.indexOf('add') != -1) {
      this.actionType = TreeActionType.AddNode;
    }
    else if (type.indexOf('edit') != -1) {
      this.actionType = TreeActionType.EditNode;
    }
    this.showActionsButtonSection = false;
    this.getEmittedCurrentSelectedNodeValueRef =
      this.eventHandlerService.getEmittedCurrentSelectedLocationNodeValue$().subscribe((node: any) => {
        if (node && this.clickedOnButtons) {
          this.clickedOnButtons = false;
          this.eventHandlerService.emitShowLocationActionsButtonSectionEvent(false);
          let showActionFormData = new ActionsFormData(this.actionType, node, true);
          this.eventHandlerService.emitShowLocationActionFormEvent(showActionFormData);
        }
      });
  }


  RemoveNode(): void {
    this.eventHandlerService.emitRemoveLocationEvent();
  }
}

  // private _showActionButtonSection = true;
  // get showActionsButtonSection(): any {
  //   return this._showActionButtonSection;
  // }
  // @Input() set showActionsButtonSection(value: any) {
  //   this._showActionButtonSection = (value !== undefined) ? value : true;
  // }
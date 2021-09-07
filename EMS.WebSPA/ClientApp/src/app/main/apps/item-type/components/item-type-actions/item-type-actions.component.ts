import { Component, OnInit, OnDestroy } from '@angular/core';
import { TreeActionType } from 'app/shared/models/tree.model';
import { Subscription } from 'rxjs';
 import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-item-type-actions',
  templateUrl: './item-type-actions.component.html',
  styleUrls: ['./item-type-actions.component.scss']
})
export class ItemTypeActionsComponent implements OnDestroy {

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
      this.eventHandlerService.getEmittedShowItemTypeActionsButtonSectionValue$().
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
      this.eventHandlerService.getEmittedCurrentSelectedItemTypeNodeValue$().subscribe((node: any) => {
        console.log("getEmittedCurrentSelectedNodeValue",node);
        if (node && this.clickedOnButtons) {
          this.clickedOnButtons = false;
          this.eventHandlerService.emitShowItemTypeActionsButtonSectionEvent(false);
          let showActionFormData = new ActionsFormData(this.actionType, node, true);
          this.eventHandlerService.emitShowItemTypeActionFormEvent(showActionFormData);
        }
      });
  }
  
  RemoveNode(): void {
    this.eventHandlerService.emitRemoveItemTypeEvent();
  }
}

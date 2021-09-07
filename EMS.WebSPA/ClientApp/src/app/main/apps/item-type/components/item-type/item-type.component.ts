import { Component, OnDestroy, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { Subscription } from 'rxjs';
import { EventHandlerService } from 'app/shared/services/event-handler.service';
  
@Component({
  selector: 'app-item-type',
  templateUrl: './item-type.component.html',
  styleUrls: ['./item-type.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ItemTypeComponent implements OnDestroy {
  showActionsForm: boolean;
  actionFormData: ActionsFormData;
  showActionsButtonSection: boolean;
  private getEmittedCurrentSelectedNodeValueRef: Subscription = null;
  constructor(private eventHandlerService: EventHandlerService) { }
  ngOnDestroy(): void {
    if (this.getEmittedCurrentSelectedNodeValueRef) { this.getEmittedCurrentSelectedNodeValueRef.unsubscribe(); }
  }
  handleShowActionForm($event) {
    this.getEmittedCurrentSelectedNodeValueRef =
      this.eventHandlerService.getEmittedCurrentSelectedItemTypeNodeValue$().
        subscribe((node: any) => { this.actionFormData = new ActionsFormData($event, node, true); });
  }

  handleCloseActionsForm($event) {
    this.showActionsButtonSection = $event;
  }
}

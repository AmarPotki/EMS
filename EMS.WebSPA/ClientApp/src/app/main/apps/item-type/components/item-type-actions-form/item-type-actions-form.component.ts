import { Component, OnInit } from '@angular/core';
import { TreeActionType } from 'app/shared/models/tree.model';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material';
 import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-item-type-actions-form',
  templateUrl: './item-type-actions-form.component.html',
  styleUrls: ['./item-type-actions-form.component.scss']
})
export class ItemTypeActionsFormComponent implements OnInit {
  nodeName = '';
  showForm = false;
  selectedItem: any;
  showActionsForm = false;
  showActionsButton = true;
  actionType: TreeActionType;
  private getEmittedActionsFormDataValueRef: Subscription = null;
  constructor(
    public snackBar: MatSnackBar,
    public eventHandlerService: EventHandlerService
  ) {}

  ngOnInit(): void {
    this.handleActionsFormDataObservable();
  }

  ngOnDestroy(): void {
    if (this.getEmittedActionsFormDataValueRef) { this.getEmittedActionsFormDataValueRef.unsubscribe(); }
  }

  handleActionsFormDataObservable(): void {
    this.getEmittedActionsFormDataValueRef =
      this.eventHandlerService.getEmittedShowItemTypeActionFormValue$().subscribe((data: ActionsFormData) => {
        console.log("data",data);
        if (!data.showActionsForm) {
          this.showActionsForm = false;
          this.eventHandlerService.emitShowItemTypeActionsButtonSectionEvent(true);
        }
        else if (data.showActionsForm) {
          this.showActionsForm = data.showActionsForm;
          this.actionType = data.actionType;
          this.selectedItem = data.selectedItem;
          (this.actionType === TreeActionType.EditNode) ? (this.nodeName = data.selectedItem.label) : this.nodeName = '';
        }
      });
  }

  closeForm(): void {
    this.nodeName = '';
    this.showActionsForm = false;
    this.eventHandlerService.emitShowItemTypeActionsButtonSectionEvent(true);
  }

  save(): void {
    if (!this.nodeName.trim()) { this.snackBar.open('وارد نمودن نام فایل الزامی است', 'بستن'); }
    else if (this.nodeName.trim()) {
      if (this.actionType === TreeActionType.AddNode) { this.AddNode(); }
      else if (this.actionType === TreeActionType.EditNode) { this.UpdateNode(); }
    }
  }

  AddNode(): void {
    this.eventHandlerService.emitAddItemTypeEvent(this.nodeName);
    this.closeForm();
  };

  UpdateNode(): void {
    this.eventHandlerService.emitUpdateItemTypeEvent(this.nodeName);
    this.closeForm();
  }

}

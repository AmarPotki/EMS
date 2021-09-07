import { Component, Input, ViewChild, Output, EventEmitter, AfterViewInit, OnDestroy, OnInit } from '@angular/core';
import { TreeActionType } from 'app/shared/models/tree.model';
 import { MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';
import { ActionsFormData } from 'app/shared/models/actoins-form-data';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-location-actions-form',
  templateUrl: './location-actions-form.component.html',
  styleUrls: ['./location-actions-form.component.scss']
})
export class LocationActionsFormComponent implements OnInit, OnDestroy {
  nodeName = '';
  showForm = false;
  selectedItem: any;
  actionType: TreeActionType;
  showActionsButton = true;
  showActionsForm = false;

  private getEmittedActionsFormDataValueRef: Subscription = null;
  constructor(
    public snackBar: MatSnackBar,
    public eventHandlerService:EventHandlerService
  ) {}
  
  ngOnInit(): void {
    this.handleActionsFormDataObservable();
  }

  ngOnDestroy(): void {
    if (this.getEmittedActionsFormDataValueRef) { this.getEmittedActionsFormDataValueRef.unsubscribe(); }
  }

  handleActionsFormDataObservable(): void {
    this.getEmittedActionsFormDataValueRef =
      this.eventHandlerService.getEmittedShowLocationActionFormValue$().subscribe((data: ActionsFormData) => {
        console.log("data in form", data);
        if (!data.showActionsForm) {
          this.showActionsForm = false;
          this.eventHandlerService.emitShowLocationActionsButtonSectionEvent(true);
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
    this.eventHandlerService.emitShowLocationActionsButtonSectionEvent(true);
  }

  save(): void {
    if (!this.nodeName.trim()) { this.snackBar.open('وارد نمودن نام فایل الزامی است', 'بستن'); }
    else if (this.nodeName.trim()) {
      if (this.actionType === TreeActionType.AddNode) { this.AddNode(); }
      else if (this.actionType === TreeActionType.EditNode) { this.UpdateNode(); }
    }
  }

  AddNode(): void {
    this.eventHandlerService.emitAddLocationEvent(this.nodeName);
    this.closeForm();
  };

  UpdateNode(): void {
    this.eventHandlerService.emitUpdateLocationEvent(this.nodeName);
    this.closeForm();
  }
}
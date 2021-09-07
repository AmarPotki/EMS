import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-item-type-dialog',
  templateUrl: './item-type-dialog.component.html',
  styleUrls: ['./item-type-dialog.component.scss']
})
export class ItemTypeDialogComponent  {
  showError = false;
  clicked = false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private eventHandlerService: EventHandlerService,
    public dialogRef: MatDialogRef<ItemTypeDialogComponent>
  ) { }

  onClose(): void {
    this.clicked = true;
    this.eventHandlerService.getEmittedCurrentSelectedItemTypeNodeValue$().
      subscribe((selectedNode: any) => {
        if (selectedNode && this.clicked) { this.dialogRef.close(); }
        else {
          this.showError = true;
          this.clicked = false;
        }
      });
  }
}

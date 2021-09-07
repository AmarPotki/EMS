import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EventHandlerService } from 'app/shared/services/event-handler.service';

@Component({
  selector: 'app-location-dialog',
  templateUrl: './location-dialog.component.html',
  styleUrls: ['./location-dialog.component.scss']
})
export class LocationDialogComponent {
  showError = false;
  clicked = false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private eventHandlerService: EventHandlerService,
    public dialogRef: MatDialogRef<LocationDialogComponent>
  ) {

   }

  onClose(): void {
    this.clicked = true;
    this.eventHandlerService.getEmittedCurrentSelectedLocationNodeValue$().
      subscribe((selectedNode: any) => {
        if (selectedNode && this.clicked) { this.dialogRef.close(); }
        else {
          this.showError = true;
          this.clicked = false;
        }
      });
  }


}

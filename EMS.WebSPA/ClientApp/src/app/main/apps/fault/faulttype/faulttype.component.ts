import { Component, OnInit, ViewEncapsulation, Inject, ViewChild, TemplateRef } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FaultTypeDto } from './fulttype.model';
import { FaulttypeService } from './faulttype.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog, MatSnackBar } from '@angular/material';
import { DialogData } from 'assets/angular-material-examples/dialog-data/dialog-data-example';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-faulttype',
  templateUrl: './faulttype.component.html',
  styleUrls: ['./faulttype.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class FaulttypeComponent implements OnInit {

  dataSource: FaultTypeDto[] | null;
  faulttype: FaultTypeDto;
  faultname: string;
  pageType: string;
  displayedColumns = ['id', 'name', 'isArchive', 'buttons'];

  @ViewChild('dialogContent')
  dialogContent: TemplateRef<any>;
  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  constructor(
    private faultTypeservice: FaulttypeService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit() {
    this.faulttype = new FaultTypeDto();
    this.faultTypeservice.getFaultType().subscribe(faulttype => {
      this.dataSource = faulttype;
    })
  }

  newFaulttype(): void {
    const dialogRef = this.dialog.open(NewFaultTypeDialog, {
      width: '450px',
      data: { partname: this.faultname }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result) {
        this.faulttype.name = result;
        this.faultTypeservice.createFaultType(this.faulttype).subscribe(re => {
          console.log(re);
          this.refresh();
          //if(re){
          this.snackBar.open('نوع خرابی جدید ثبت شد', 'بستن', {
            duration: 2000,
          });
          // }
        })
      }
    });
  }

  editFaulttype(faulttype: FaultTypeDto): void {
    this.faultname = faulttype.name;
    const dialogRef = this.dialog.open(NewFaultTypeDialog, {
      width: '450px',
      data: { partname: faulttype.name }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result) {
        this.faulttype.name = result;
        this.faulttype.id = faulttype.id;
        this.faultTypeservice.updatefaultType(this.faulttype).subscribe(re => {
          console.log(re);
          this.refresh();
          //if(re){
          this.snackBar.open('نوع خرابی بروزرسانی شد', 'بستن', {
            duration: 2000,
          });
          // }
        })
      }
    });
  }

  deleteFaulttype(faulttype: FaultTypeDto): void {
    this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'آیا مطمئن هستید که میخواهید حذف کنید ؟';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.faultTypeservice.deleteFaultType(faulttype).subscribe(result => {
          this.snackBar.open('نوع خرابی "'+ faulttype.name +'" حذف شد', 'بستن', {
            duration: 2000,
          });
        })
      }
      this.confirmDialogRef = null;
    });
  }

  refresh() {
    this.faultTypeservice.getFaultType().subscribe(faulttype => {
      this.dataSource = faulttype;
    })
    //this.changeDetectorRefs.detectChanges();
  }


}

@Component({
  selector: 'faulttype-dialog',
  templateUrl: 'faulttype-dialog.html',
})
export class NewFaultTypeDialog {

  constructor(
    public dialogRef: MatDialogRef<NewFaultTypeDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

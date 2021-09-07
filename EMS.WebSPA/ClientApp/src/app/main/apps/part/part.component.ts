import { Component, OnInit, ViewChild, ElementRef, Inject, ViewEncapsulation, ChangeDetectorRef, TemplateRef } from '@angular/core';
import { PartsDto } from './parts.model';
import { MatPaginator, MatSort, MatDialog, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { PartService } from './part.service';
import { DialogData } from 'assets/angular-material-examples/dialog-data/dialog-data-example';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-part',
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class PartComponent implements OnInit {

    dataSource: PartsDto[] | null;
    Parts:PartsDto;
    partname:string;
    pageType: string;
    displayedColumns = ['name', 'isArchive', 'buttons'];

    @ViewChild('dialogContent')
    dialogContent: TemplateRef<any>;
    dialogRef: any;
    confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

    @ViewChild(MatPaginator)
    paginator: MatPaginator;

    @ViewChild(MatSort)
    sort: MatSort;

    @ViewChild('filter')
    filter: ElementRef;

  constructor(
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private partsService:PartService,
    private changeDetectorRefs: ChangeDetectorRef,
    ) { }

  ngOnInit() {
    this.Parts = new PartsDto();
    this.partsService.getParts().subscribe(parts=>{
      this.dataSource = parts;
    })
  }

  newPart():void{
    const dialogRef = this.dialog.open(NewPartDialog, {
      width: '250px',
      data:{partname: this.partname}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if(result){
        this.Parts.name = result;
        this.partsService.createPart(this.Parts).subscribe(re=>{
          console.log(re);
          this.refresh();
          //if(re){
            this.snackBar.open('قطعه جدید ثبت شد', 'بستن', {
              duration: 2000,
            });
         // }
        })
      }
    });

  }

  editPart(part:PartsDto):void{
    //console.log(part);
    this.partname = part.name;
    const dialogRef = this.dialog.open(NewPartDialog, {
      width: '250px',
      data:{partname: part.name}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if(result){
        this.Parts.name = result;
        this.Parts.id = part.id;
        this.partsService.updatePart(this.Parts).subscribe(re=>{
          console.log(re);
          this.refresh();
          //if(re){
            this.snackBar.open('قطعه جدید ویرایش شد', 'بستن', {
              duration: 2000,
            });
         // }
        })
      }
    });
  }

  refresh() {
    this.partsService.getParts().subscribe(parts=>{
      this.dataSource = parts;
    })
    this.changeDetectorRefs.detectChanges();
  }

  deletePart(part:PartsDto){

    this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
      disableClose: false
  });

  this.confirmDialogRef.componentInstance.confirmMessage = 'آیا مطمئن هستید که میخواهید حذف کنید ؟';

  this.confirmDialogRef.afterClosed().subscribe(result => {
      if ( result )
      {
        this.partsService.deletePart(part).subscribe(result=>{
          this.snackBar.open('قطعه مورد نظر حذف شد', 'بستن', {
            duration: 2000,
          });
        })
      }
      this.confirmDialogRef = null;
  });
  }

}

@Component({
  selector: 'newpart-dialog',
  templateUrl: 'newpart-dialog.html',
})
export class NewPartDialog {

  constructor(
    public dialogRef: MatDialogRef<NewPartDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}
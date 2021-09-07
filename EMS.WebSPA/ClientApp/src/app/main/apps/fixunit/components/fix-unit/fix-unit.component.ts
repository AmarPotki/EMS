import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FixUnitDto } from '../../models/fixunit.model';
import { FixUnitService } from '../../service/fix-unit.service';
import { Router } from '@angular/router';
import { MatSnackBar, MatDialog } from '@angular/material';
import { DeleteDialogComponent } from 'app/shared/components/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-fix-unit',
  templateUrl: './fix-unit.component.html',
  styleUrls: ['./fix-unit.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class FixUnitComponent implements OnInit {
 
  dataSource: FixUnitDto[] | null;
  fixunit: FixUnitDto;
  pageType: string;
  displayedColumns = ['id', 'title', 'itemType','location', 'buttons'];

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private fixUnitService: FixUnitService,
  ) { }
  ngOnInit() {
    this.fixunit = new FixUnitDto();
    this.getFixUnits();
  }

  private getFixUnits() {
    this.fixUnitService.getFixUnits().subscribe(fixUnits => {
       this.dataSource = fixUnits;
    });
  }

  editFixUnit(fixUnit: any) {
    this.router.navigate([`apps/fixunit/add/${fixUnit.id}`]);
  }

  deleteFixUnit(fixUnit: any) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fixUnitService.deleteFixUnit(+fixUnit['id']).subscribe(
          _ => {
            this.snackBar.open('حذف واحد تعمیر باموفقیت انجام شد', 'بستن');
          },
          _ => {
            this.snackBar.open('خطایی در حذف واحد تعمیر بوجودآمده است', 'بستن');
          });
      }
    });
  }

  memberManagement(fixUnit:any){
    this.router.navigate([`apps/fixunit/members/${fixUnit.id}`]);
  }
}

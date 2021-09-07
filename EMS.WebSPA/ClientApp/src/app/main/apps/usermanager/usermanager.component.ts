import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatPaginator, MatSort, MAT_DIALOG_DATA, MatDialogRef, MatDialog, MatSnackBar } from '@angular/material';
import { fuseAnimations } from '@fuse/animations';
import { UserDto } from './usermanager.model';
import { UsermanagerService } from './usermanager.service';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { DialogData } from 'assets/angular-material-examples/dialog-data/dialog-data-example';

@Component({
  selector: 'app-usermanager',
  templateUrl: './usermanager.component.html',
  styleUrls: ['./usermanager.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class UsermanagerComponent implements OnInit {

  pageType: string;
  productForm: FormGroup;

  dataSource: UserDto[] | null;
  displayedColumns = ['id', 'name', 'role', 'username', 'lockoutEnabled', 'buttons'];

  @ViewChild(MatPaginator)
  paginator: MatPaginator;

  @ViewChild(MatSort)
  sort: MatSort;

  @ViewChild('filter')
  filter: ElementRef;

  /**
     * Constructor
     *
     * @param {EcommerceProductService} _ecommerceProductService
     * @param {FormBuilder} _formBuilder
     * @param {Location} _location
     * @param {MatSnackBar} _matSnackBar
     */
  constructor(
    private userService: UsermanagerService,
    private _location: Location,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.userService.getUsers().subscribe(result => {
      this.dataSource = result;
    })
  }
  editUser(id: string): void {

    this.router.navigate(['/apps/usermanager/new/', id]);
  }

  resetPassword(Id: string) {
    const dialogRef = this.dialog.open(ResetPasswordDialog, {
      width: '250px',
      data: { userIdentity: Id }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result) {
        this.userService.resetPassword(Id).subscribe(re => {
          console.log(re);

          //if(re){
          this.snackBar.open('بازنشانی کلمه عبور با موفقیت انجام شد', 'بستن', {
            duration: 2000,
          });
          // }
        })
      }
    });
  }

  lockOutUser(user: UserDto): void {
    let Id = user.id;
    let modaltitle;
    if (user.lockoutEnabled) {
      modaltitle = 'فعال کردن کاربر';
    } else {
      modaltitle = 'غیر فعال کردن کاربر';
    }
    const dialogRef = this.dialog.open(LockAndUnlockOutUser, {
      width: '250px',
      data: { userIdentity: Id, message: modaltitle }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result) {
        if (user.lockoutEnabled) {
          this.userService.unlockUser(Id).subscribe(re=>{
            this.snackBar.open('کاربر فعال شد', 'بستن', {
              duration: 2000,
            });
            this.userService.getUsers().subscribe(result => {
              this.dataSource = result;
            })
          })
         } else {
          this.userService.lockUser(Id).subscribe(re => {
            this.snackBar.open('کاربر غیر فعال شد', 'بستن', {
              duration: 2000,
            });
            this.userService.getUsers().subscribe(result => {
              this.dataSource = result;
            })
          })
        }
      }
    })
  }
}


@Component({
  selector: 'resetPassword-dialog',
  templateUrl: 'resetPassword-dialog.html',
})
export class ResetPasswordDialog {

  constructor(
    public dialogRef: MatDialogRef<ResetPasswordDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

@Component({
  selector: 'lockAndUnlockOutUser-dialog',
  templateUrl: 'lockAndUnlockOutUser-dialog.html',
})
export class LockAndUnlockOutUser {

  constructor(
    public dialogRef: MatDialogRef<LockAndUnlockOutUser>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

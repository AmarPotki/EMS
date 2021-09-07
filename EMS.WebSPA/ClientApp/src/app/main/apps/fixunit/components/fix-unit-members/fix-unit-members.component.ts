import { Component, OnInit } from '@angular/core';
import { UsermanagerService } from 'app/main/apps/usermanager/usermanager.service';
import { UserDto } from 'app/main/apps/usermanager/usermanager.model';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatSnackBar } from '@angular/material';
import { FixUnitService } from '../../service/fix-unit.service';
import { DeleteDialogComponent } from 'app/shared/components/delete-dialog/delete-dialog.component';
import { Member } from '../../models/fix-unit-member';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-fix-unit-members',
  templateUrl: './fix-unit-members.component.html',
  styleUrls: ['./fix-unit-members.component.scss']
})
export class FixUnitMembersComponent implements OnInit {
  fixUnitId = 0;
  users: UserDto[] = [];
  members: UserDto[] = [];
  addMemberForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private fixUnitService: FixUnitService,
    private userService: UsermanagerService,
  ) {
    this.createForm();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.fixUnitId = +params['id'];
      this.getFixUnitMembers();
      this.getUsers();
    });
  }
  private createForm() {
    this.addMemberForm = this.fb.group({
      selectedMember: ['', Validators.required]
    });
  }

  private getUsers() {
    this.userService.getUsers().
      subscribe((userList: UserDto[]) => {
        this.users = userList;
      });
  }

  getFixUnitMembers(): any {
    this.fixUnitService.getFixUnitMembers(this.fixUnitId).subscribe(
      (members: UserDto[]) => {
        this.members = members; console.log(members);
      },
      () => { this.snackBar.open('خطایی در دریافت اعضا بوجود آمده است', 'بستن'); });
  }

  onSearchOwner(event) {
  }
  addMember() {
    let memeberGuid = this.addMemberForm.value.selectedMember;
    let member = new Member(memeberGuid, this.fixUnitId);
    this.fixUnitService.addFixUnitMember(member).subscribe(
      _ => {
        let newMember = this.users.find(x => x.id == memeberGuid);
        this.members.push(newMember);
        this.addMemberForm.controls['selectedMember'].setValue('');
        this.snackBar.open('فرد مورد نظر باموفقیت اضافه شد', 'بستن');
      },
      _ => {
        this.snackBar.open('خطایی در افزودن فرد انتخابی بوجود آمده است', 'بستن');
      });
  }


  removeMember(selectedItem: any) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let member = new Member(selectedItem['id'], this.fixUnitId);
        this.fixUnitService.removeFixUnitMember(member).subscribe(
          _ => {
            this.members = this.members.filter(x => x.id !== member.userGuid);
            this.snackBar.open('فرد مورد نظر باموفقیت حذف شد', 'بستن');
          },
          _ => {
            this.snackBar.open('خطایی در حذف فرد انتخابی بوجود آمده است', 'بستن');
          });
      };
    });
  }
}

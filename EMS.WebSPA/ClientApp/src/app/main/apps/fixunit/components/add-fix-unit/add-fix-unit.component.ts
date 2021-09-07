import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FixUnitDto } from '../../models/fixunit.model';
import { UserDto } from 'app/main/apps/usermanager/usermanager.model';
import { UsermanagerService } from 'app/main/apps/usermanager/usermanager.service';
import { FixUnitService } from '../../service/fix-unit.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { ItemTypeDialogComponent } from '../item-type-dialog/item-type-dialog.component';
import { EventHandlerService } from 'app/shared/services/event-handler.service';
import { LocationDialogComponent } from '../location-dialog/location-dialog.component';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-fix-unit',
  templateUrl: './add-fix-unit.component.html',
  styleUrls: ['./add-fix-unit.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class AddFixUnitComponent implements OnInit {
  fixUnitId = 0;
  pageType: string;
  users: UserDto[] = [];
  members: UserDto[] = [];
  fixUnitForm: FormGroup;
  fixUnit = new FixUnitDto();
  selectedLocation = null;
  selectedItemType = null;
  @ViewChild('locationInput') locationInput: ElementRef;
  @ViewChild('itemTypeInput') itemTypeInput: ElementRef;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private fixUnitService: FixUnitService,
    private userService: UsermanagerService,
    private eventHandlerService: EventHandlerService
  ) {
    this.createForm();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.fixUnitId = +params['id'];
      if (this.fixUnitId > 0) {
        this.pageType = 'edit';
        this.getFixUnit();
      }
      else {
        this.pageType = 'new';
      }
      this.getUsers();
    });
  }

  private getUsers() {
    this.userService.getUsers().
      subscribe((userList: UserDto[]) => {
        this.users = userList;
        this.members = userList;
      });
  }

  private getFixUnit() {
    this.fixUnitService.getFixUnit(this.fixUnitId).subscribe((fixUnit: any) => {
      this.fixUnit = fixUnit;
      this.AddIdControlToFixUnitForm();
      this.bindValueToFixUnitForm(fixUnit);
    });
  }

  private AddIdControlToFixUnitForm() {
    this.fixUnitForm.addControl('Id', this.fb.control(this.fixUnitId, Validators.required));
  }

  private bindValueToFixUnitForm(fixUnit: any) {
    this.fixUnitForm.patchValue({
      locationId: fixUnit.locationId,
      itemTypeId: fixUnit.itemTypeId,
      owner: fixUnit.owner,
      title: fixUnit.title,
      description: fixUnit.description
    });
    this.locationInput.nativeElement.value = fixUnit.location;
    this.itemTypeInput.nativeElement.value = fixUnit.itemType;
  }

  private createForm(): void {
    this.fixUnitForm = this.fb.group({
      locationId: [0, [Validators.required, Validators.min(1)]],
      itemTypeId: [1, [Validators.required, Validators.min(1)]],
      owner: ['', Validators.required],
      members: [[], Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required]
    });
  }


  onSearchOwner(userName): void {
    this.userService.searchUsers(userName['term']).subscribe(
      (users: UserDto[]) => { this.users = users; },
      () => console.log("error"));
  }


  onSearchMember(userName): void {
    this.userService.searchUsers(userName['term']).subscribe((users: UserDto[]) => { this.members = users; });
  }

  displayFn(user?: UserDto): string | undefined {
    return user ? user.name : undefined;
  }

  openLocationDialog(): void {
    const locationDialogRef = this.dialog.open(LocationDialogComponent, { width: '19%' });
    locationDialogRef.afterClosed().subscribe(_ => {
      this.eventHandlerService.getEmittedCurrentSelectedLocationNodeValue$().
        subscribe((node: any) => {
          if (node) {
            this.selectedLocation = node;
            this.fixUnitForm.controls['locationId'].setValue(parseInt(node['id']));
            this.locationInput.nativeElement.value = node.label;
          }
        });
    });
  }

  openItemTypeDialog(): void {
    const itemTypeDialogRef = this.dialog.open(ItemTypeDialogComponent, { width: '250px' });
    itemTypeDialogRef.afterClosed().subscribe(_ => {
      this.eventHandlerService.getEmittedCurrentSelectedItemTypeNodeValue$().
        subscribe((node: any) => {
          if (node) {
            this.selectedItemType = node;
            this.fixUnitForm.controls['itemTypeId'].setValue(parseInt(node['id']));
            this.itemTypeInput.nativeElement.value = node.label;
          }
        });
    });
  }

  createFixUnit(): void {
    if (!this.fixUnitForm.valid) { return; }
    this.fixUnitService.createFixunit(this.fixUnitForm.value).
      subscribe(
        _ => {
          this.snackBar.open('واحد جدید با موفقیت افزوده شد', 'بستن');
          this.router.navigate(['/apps/fixunit']);
        },
        _ => this.snackBar.open('متاسفانه واحد جدید افزوده نشد', 'بستن')
      );
  }

  updateFixunit(): void {
    this.fixUnitService.updateFixUnit(this.fixUnitForm.value).
      subscribe(
        _ => {
          this.snackBar.open('واحد موردنظر با موفقیت ویرایش شد', 'بستن');
          this.router.navigate(['/apps/fixunit']);
        },
        _ => this.snackBar.open('متاسفانه واحد نظر ویرایش نشد', 'بستن')
      );
  }
}

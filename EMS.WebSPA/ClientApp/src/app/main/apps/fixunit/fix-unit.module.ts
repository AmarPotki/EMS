import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule,
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule, MatCardModule
} from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { NgxDnDModule } from '@swimlane/ngx-dnd';

import { FixUnitService } from './service/fix-unit.service';
import { FixUnitComponent } from './components/fix-unit/fix-unit.component';
import { AddFixUnitComponent } from './components/add-fix-unit/add-fix-unit.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { LocationDialogComponent } from './components/location-dialog/location-dialog.component';
import { ItemTypeDialogComponent } from './components/item-type-dialog/item-type-dialog.component';
import { LocationModule } from '../location/location.module';
import { ItemTypeModule } from '../item-type/item-type.module';
import { FixUnitMembersComponent } from './components/fix-unit-members/fix-unit-members.component';

const routes = [
  {
    path: 'fixunit',
    component: FixUnitComponent
  },
  {
    path: 'fixunit/add/:id',
    component: AddFixUnitComponent
  },
  {
    path: 'fixunit/members/:id',
    component: FixUnitMembersComponent
  }
]
@NgModule({
  declarations: [
    FixUnitComponent,
    AddFixUnitComponent,
    LocationDialogComponent,
    ItemTypeDialogComponent,
     FixUnitMembersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FuseSharedModule,
    FuseCountdownModule,
    NgxDnDModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatRippleModule,
    MatSelectModule,
    MatToolbarModule,
    MatChipsModule,
    MatTabsModule,
    MatTableModule,
    MatAutocompleteModule,
    NgSelectModule,
    LocationModule,
    ItemTypeModule,
    MatCardModule
  ],
  providers: [
    FixUnitService
  ],
  entryComponents: [
    ItemTypeDialogComponent,
    LocationDialogComponent
  ]
})
export class FixunitModule { }
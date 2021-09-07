import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule, 
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { FixfaultComponent } from './fixfault.component';
import { FixfaultService } from './fixfault.service';
import { MyfixunitComponent } from './sidebars/myfixunit/myfixunit.component';
import { FuseSidebarModule } from '@fuse/components';
import { FixfaultListComponent } from './fixfault-list/fixfault-list.component';
import { FixfaultListItemComponent } from './fixfault-list/fixfault-list-item/fixfault-list-item.component';
import { FixfaultDetailComponent } from './fixfault-list/fixfault-detail/fixfault-detail.component';

const routes = [
  {
      path     : 'fixfault',
      component: FixfaultComponent
  },
  {
    path     : 'fixfault/:id',
    component: FixfaultComponent
}
]

@NgModule({
  declarations: [
    FixfaultComponent,
    MyfixunitComponent,
    FixfaultListComponent,
    FixfaultListItemComponent,
    FixfaultDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FuseSharedModule,
    FuseCountdownModule,
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
    NgxDnDModule,
    FuseSidebarModule
  ],
  providers      : [
    FixfaultService
  ],
})
export class FixfaultModule { }

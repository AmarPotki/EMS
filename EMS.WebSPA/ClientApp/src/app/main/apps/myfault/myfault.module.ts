import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatefaultComponent } from './createfault/createfault.component';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule, 
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { MyfaultComponent } from './myfault.component';
import { MyfaultService } from './myfault.service';
import { MyfaultDetailComponent } from './myfault-detail/myfault-detail.component';

const routes = [
  {
      path     : 'myfault',
      component: MyfaultComponent
  },
  {
    path    :'myfault/createfault',
    component: CreatefaultComponent
  },
  {
    path    :'myfault/myfault-detail/:id',
    component: MyfaultDetailComponent
  }
]

@NgModule({
  declarations: [
    CreatefaultComponent,
    MyfaultComponent,
    MyfaultDetailComponent
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
    MatAutocompleteModule
  ],
  providers      : [
    MyfaultService
  ],
  entryComponents:[
  ]
})
export class MyfaultModule { }

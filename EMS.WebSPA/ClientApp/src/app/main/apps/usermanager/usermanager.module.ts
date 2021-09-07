import { NgModule } from '@angular/core';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UsermanagerComponent, ResetPasswordDialog, LockAndUnlockOutUser } from './usermanager.component';
import { UsermanagerService } from './usermanager.service';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule, 
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule } from '@angular/material';
import { NewComponent } from './new/new.component';

const routes = [
  {
      path     : 'usermanager',
      component: UsermanagerComponent
  },
  {
      path      : 'usermanager/new',
      component : NewComponent
  },
  {
      path     : 'usermanager/new/:id',
      component: NewComponent,
  }
]
@NgModule({
  declarations: [
    UsermanagerComponent,
    NewComponent,
    ResetPasswordDialog,
    LockAndUnlockOutUser
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
  ],
  providers      : [
    UsermanagerService
  ],
  entryComponents: [ResetPasswordDialog, LockAndUnlockOutUser]
})
export class UsermanagerModule { }

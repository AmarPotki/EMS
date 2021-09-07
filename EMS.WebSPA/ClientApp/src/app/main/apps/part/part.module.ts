import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule, 
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule, FuseConfirmDialogModule } from '@fuse/components';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { PartComponent, NewPartDialog } from './part.component';
import { PartService } from './part.service';
const routes = [
  {
      path     : 'part',
      component: PartComponent
  }
]
@NgModule({
  declarations: [
    PartComponent,
    NewPartDialog
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
    FuseConfirmDialogModule
  ],
  providers      : [
    PartService
  ],
  entryComponents:[
    NewPartDialog
  ]
})
export class PartModule { }

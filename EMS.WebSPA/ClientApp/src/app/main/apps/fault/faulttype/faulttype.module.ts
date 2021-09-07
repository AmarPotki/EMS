import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule, 
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { FaulttypeComponent, NewFaultTypeDialog } from './faulttype.component';
import { FaulttypeService } from './faulttype.service';

const routes = [
  {
      path     : 'faulttype',
      component: FaulttypeComponent
  }
]

@NgModule({
  declarations: [
    FaulttypeComponent,
    NewFaultTypeDialog
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
    FaulttypeService
  ],
  entryComponents:[
    NewFaultTypeDialog
  ]
})
export class FaulttypeModule { }

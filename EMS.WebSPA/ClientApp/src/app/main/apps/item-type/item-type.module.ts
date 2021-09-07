import { NgModule } from '@angular/core';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { CommonModule } from '@angular/common';
import {
  MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule,
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatAutocompleteModule, MatCardModule
} from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { ItemTypeComponent } from './components/item-type/item-type.component';
import { ItemtypeService } from './services/itemtype.service';
import { ItemTypeActionsComponent } from './components/item-type-actions/item-type-actions.component';
import { ItemTypeActionsFormComponent } from './components/item-type-actions-form/item-type-actions-form.component';
import { ItemTypeListComponent } from './components/item-type-list/item-type-list.component';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';

const routes = [
  {
    path: 'itemtype',
    component: ItemTypeComponent
  }
]

@NgModule({
  declarations: [
    ItemTypeComponent,
    ItemTypeActionsComponent,
    ItemTypeActionsFormComponent,
    ItemTypeListComponent,
      // jqxTreeComponent
   ],
  imports: [
    CommonModule,
    NgxDnDModule,
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
    MatCardModule
  ],
  exports:[
    ItemTypeListComponent
  ],
  providers: [
    ItemtypeService
  ]
})
export class ItemTypeModule { }

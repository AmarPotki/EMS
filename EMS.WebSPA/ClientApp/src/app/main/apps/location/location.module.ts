import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatButtonModule, MatCheckboxModule, MatDialogModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatInputModule,
  MatRippleModule, MatSelectModule, MatToolbarModule, MatChipsModule, MatTabsModule, MatTableModule, MatTreeModule, MatProgressBarModule,
  MatProgressSpinnerModule,
  MatCardModule
} from '@angular/material';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseCountdownModule } from '@fuse/components';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { LocationService } from './services/location.service';

import { LocationComponent } from './components/location/location.component';
import { LocationListComponent } from './components/location-list/location-list.component';
import { LocationActionsComponent } from './components/location-actions/location-actions.component';
import { LocationActionsFormComponent } from './components/location-actions-form/location-actions-form.component';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';


const routes = [{ path: 'location', component: LocationComponent }];

@NgModule({
  declarations: [
    LocationComponent,
    LocationListComponent, LocationActionsComponent, LocationActionsFormComponent,
    jqxTreeComponent
   ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
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
    MatTreeModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatCardModule
  ],
  exports: [
    LocationListComponent,
    jqxTreeComponent
  ],
  providers: [
    LocationService,
  ]
})
export class LocationModule { }

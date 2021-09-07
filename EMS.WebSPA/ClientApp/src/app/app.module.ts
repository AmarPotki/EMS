import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule, MatIconModule, DateAdapter, MatChipsModule } from '@angular/material';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { FakeDbService } from 'app/fake-db/fake-db.service';
import { AppComponent } from 'app/app.component';
import { AppStoreModule } from 'app/store/store.module';
import { LayoutModule } from 'app/layout/layout.module';
import localeFa from '@angular/common/locales/fa-IR';
import { registerLocaleData } from '@angular/common';
registerLocaleData(localeFa);
import { PersianDatePickerModule } from './main/persianDatePicker/persian-datepicker.module';
import { JalaliMomentDateAdapter } from './main/persian-calendar-provider/jalali-moment-date-adapter';
import { CalendarModule, CalendarDateFormatter } from 'angular-calendar';
import { CalendarJalaliDateFormatter } from './main/persian-calendar-provider/jalali_moment_formats';
import { jqxBarGaugeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxbargauge';
import { TreeService } from './shared/services/tree.service';
import { ConfigurationService } from './shared/services/configuration.service';
import { FormsModule } from '@angular/forms';

import { DeleteDialogComponent } from './shared/components/delete-dialog/delete-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';
import { NgSelectModule } from '@ng-select/ng-select';
import { LocationModule } from './main/apps/location/location.module';
import { ItemTypeModule } from './main/apps/item-type/item-type.module';
import { AppsModule } from './main/apps/apps.module';
import { SharedModule } from './shared/shared.module';


const appRoutes: Routes = [
    {
        path: 'apps',
        loadChildren: './main/apps/apps.module#AppsModule'
    },
    {
        path: 'pages',
        loadChildren: './main/pages/pages.module#PagesModule'
    },
    {
        path: 'ui',
        loadChildren: './main/ui/ui.module#UIModule'
    },
    {
        path: 'documentation',
        loadChildren: './main/documentation/documentation.module#DocumentationModule'
    },
    {
        path: 'angular-material-elements',
        loadChildren: './main/angular-material-elements/angular-material-elements.module#AngularMaterialElementsModule'
    },
    {
        path: '**',
        redirectTo: 'apps/dashboards/analytics'
    }
];

@NgModule({
    declarations: [
        AppComponent,
        // LocationComponent,
        // jqxBarGaugeComponent
        DeleteDialogComponent
    ],
    imports: [
        BrowserModule,
        // shared
      SharedModule.forRoot(),
        BrowserAnimationsModule,
        HttpClientModule,
        PersianDatePickerModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),
        InMemoryWebApiModule.forRoot(FakeDbService, {
            delay: 0,
            passThruUnknownUrl: true
        }),
        CalendarModule.forRoot({
            provide: DateAdapter,
            useFactory: function () {
                return new JalaliMomentDateAdapter('fa');
            }
        }, {
                dateFormatter: {
                    provide: CalendarDateFormatter,
                    useClass: CalendarJalaliDateFormatter
                }
            }),
          

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,
         // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        FuseThemeOptionsModule,

        // App modules
        LayoutModule,
        AppStoreModule,
        LocationModule,
        ItemTypeModule
    ],
    providers: [
        { provide: LOCALE_ID, useValue: 'fa-IR' },
        TreeService,
        ConfigurationService
    ],
    entryComponents: [
        DeleteDialogComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
}

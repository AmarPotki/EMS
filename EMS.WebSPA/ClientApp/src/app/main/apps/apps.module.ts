import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { UsermanagerModule } from './usermanager/usermanager.module';
import { LocationModule } from './location/location.module';
import { FixunitModule } from './fixunit/fix-unit.module';
import { PartModule } from './part/part.module';
import { ItemTypeModule } from './item-type/item-type.module';
import { FaulttypeModule } from './fault/faulttype/faulttype.module';
import { MyfaultModule } from './myfault/myfault.module';
import { FixfaultModule } from './fixfault/fixfault.module';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree';

const routes = [
    {
        path: 'dashboards/analytics',
        loadChildren: './dashboards/analytics/analytics.module#AnalyticsDashboardModule'
    },
    {
        path: 'dashboards/project',
        loadChildren: './dashboards/project/project.module#ProjectDashboardModule'
    },
    {
        path: 'location',
        loadChildren: './location/location.module#LocationModule'
    },
    {
        path: 'mail-ngrx',
        loadChildren: './mail-ngrx/mail.module#MailNgrxModule'
    },
    {
        path: 'chat',
        loadChildren: './chat/chat.module#ChatModule'
    },
    {
        path: 'calendar',
        loadChildren: './calendar/calendar.module#CalendarModule'
    },
    {
        path: 'e-commerce',
        loadChildren: './e-commerce/e-commerce.module#EcommerceModule'
    },
    {
        path: 'academy',
        loadChildren: './academy/academy.module#AcademyModule'
    },
    {
        path: 'todo',
        loadChildren: './todo/todo.module#TodoModule'
    },
    {
        path: 'file-manager',
        loadChildren: './file-manager/file-manager.module#FileManagerModule'
    },
    {
        path: 'contacts',
        loadChildren: './contacts/contacts.module#ContactsModule'
    },
    {
        path: 'scrumboard',
        loadChildren: './scrumboard/scrumboard.module#ScrumboardModule'
    },
    {
        path: 'part',
        loadChildren: './part/part.module#PartModule'
    },
    {
        path: 'itemtype',
        loadChildren: './item-type/item-type.module#ItemTypeModule'
    },
    {
        path: 'faulttype',
        loadChildren: './fault/faulttype/faulttype.module#FaulttypeModule'
    },
    {
        path        : 'myfault',
        loadChildren: './myfault/myfault.module#MyfaultModule'
    },
    {
        path        : 'fixfault',
        loadChildren: './fixfault/fixfault.module#FixfaultModule'
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        FuseSharedModule,
        UsermanagerModule,
        LocationModule,
        FixunitModule,
        PartModule,
        ItemTypeModule,
        FaulttypeModule,
        MyfaultModule,
        FixfaultModule
    ],
    declarations: []
})
export class AppsModule {
}

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatIconModule, MatMenuModule, MatToolbarModule } from '@angular/material';

import { FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';

import { ToolbarComponent } from 'app/layout/components/toolbar/toolbar.component';
import { SharedModule } from 'app/shared/shared.module';

@NgModule({
    declarations: [
        ToolbarComponent
    ],
    imports: [
        RouterModule, SharedModule,
        MatButtonModule,
        MatIconModule,
        MatMenuModule,
        MatToolbarModule,
        FuseSharedModule,
        FuseSearchBarModule,
        FuseShortcutsModule,
    ],
    exports: [
        ToolbarComponent, SharedModule
    ]
})
export class ToolbarModule {
}

import { Component, OnInit } from '@angular/core';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { ActivatedRoute } from '@angular/router';
import { FixfaultService } from './fixfault.service';


@Component({
  selector: 'app-fixfault',
  templateUrl: './fixfault.component.html',
  styleUrls: ['./fixfault.component.scss']
})
export class FixfaultComponent implements OnInit {

  constructor(
    private _fuseSidebarService: FuseSidebarService,
    private route: ActivatedRoute,
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private fixfaultService: FixfaultService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let fixUnitId = params['id'];
      if(params['id']){
        //console.log(fixUnitId);
      }
    })
  }

  toggleSidebar(name): void
  {
      this._fuseSidebarService.getSidebar(name).toggleOpen();
  }

}

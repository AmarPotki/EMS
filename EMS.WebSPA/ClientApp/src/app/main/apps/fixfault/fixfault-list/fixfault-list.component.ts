import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FixFaultDto } from '../fixfault.model';
import { FixfaultService } from '../fixfault.service';
import { ActivatedRoute, Router, Event, NavigationEnd, RouteConfigLoadEnd } from '@angular/router';
import { FaultListDto } from '../../myfault/myfault.model';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-fixfault-list',
  templateUrl: './fixfault-list.component.html',
  styleUrls: ['./fixfault-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class FixfaultListComponent implements OnInit {

  fixfaults: FaultListDto;
  constructor(
    private fixfulatService: FixfaultService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // this.router.events.subscribe((event: Event) => 
    //   if (event instanceof NavigationEnd) {
    //   }
    // )

   }

  ngOnInit() {
    // this.router.events.subscribe((event: Event) => {
    //   if (event instanceof RouteConfigLoadEnd) {
        this.route.params.subscribe(params => {
          let fixUnitId = params['id'];
          if (params['id']) {
            this.fixfulatService.getMyFault(fixUnitId, 10, 0).subscribe(result => {
              //console.log(result);
              this.fixfaults = result
            })
          }
        })
    //   }
    // })
  }

  readFixfault(fixfault:FixFaultDto):void{
    this.fixfulatService.setCurrentFixfault(fixfault);
  }

}

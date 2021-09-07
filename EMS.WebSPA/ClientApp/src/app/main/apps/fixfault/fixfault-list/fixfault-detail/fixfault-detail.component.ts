import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FixFaultDto } from '../../fixfault.model';
import { FixfaultService } from '../../fixfault.service';

@Component({
  selector: 'app-fixfault-detail',
  templateUrl: './fixfault-detail.component.html',
  styleUrls: ['./fixfault-detail.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class FixfaultDetailComponent implements OnInit {

  fixfault:FixFaultDto;

  constructor(
    private fixfaultService: FixfaultService
  ) { }

  ngOnInit() {
    this.fixfaultService.CurrentFixfault.subscribe(fixfault=>{
      console.log('detail fixfault:',fixfault);
      this.fixfault = fixfault;
    })
  }

}

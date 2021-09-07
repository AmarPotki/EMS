import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { FixFaultDto } from '../../fixfault.model';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-fixfault-list-item',
  templateUrl: './fixfault-list-item.component.html',
  styleUrls: ['./fixfault-list-item.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class FixfaultListItemComponent implements OnInit {

  @Input() fixfault: FixFaultDto;
  constructor() { }

  ngOnInit() {
    console.log(this.fixfault);
    
  }

}

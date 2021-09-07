import { Component, OnInit } from '@angular/core';
import { MyfaultService } from '../myfault.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-myfault-detail',
  templateUrl: './myfault-detail.component.html',
  styleUrls: ['./myfault-detail.component.scss']
})
export class MyfaultDetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private myFaultService: MyfaultService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let faultId = params['id'];
      this.myFaultService.getFaultDetail(faultId).subscribe(fault => {
        console.log(fault);

      })
    })
  }


}

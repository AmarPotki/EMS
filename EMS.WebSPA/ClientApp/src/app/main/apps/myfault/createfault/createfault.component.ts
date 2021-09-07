import { Component, OnInit } from '@angular/core';
import { FaultDto, AddFaultDto } from '../myfault.model';
import { ActivatedRoute } from '@angular/router';
import { MyfaultService } from '../myfault.service';

@Component({
  selector: 'app-createfault',
  templateUrl: './createfault.component.html',
  styleUrls: ['./createfault.component.scss']
})
export class CreatefaultComponent implements OnInit {

  fault: AddFaultDto = null;
  pageType: string;
  constructor(
    private route: ActivatedRoute,
    private myFaultService: MyfaultService
  ) { }

  ngOnInit() {
    this.fault = new AddFaultDto();
    this.route.params.subscribe(params => {
      let userId = params['id'];
      if (params['id']) {
        this.pageType = 'edit'

      } else {
        this.pageType = 'new';
      }
    })
    
  }

  saveFault(): void {
    this.myFaultService.createFault(this.fault).subscribe(result => {
      console.log(result);
    }, error => {
      console.log(error);

    }
    )
  }

  updateFault(): void {

  }

}

import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FaulttypeService } from '../fault/faulttype/faulttype.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { FaultDto } from './myfault.model';
import { MyfaultService } from './myfault.service';

@Component({
  selector: 'app-myfault',
  templateUrl: './myfault.component.html',
  styleUrls: ['./myfault.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class MyfaultComponent implements OnInit {

  dataSource: FaultDto[] | null;
  adataSource: FaultDto[] | null;
  fault: FaultDto;
  faultname: string;
  pageType: string;
  displayedColumns = ['id', 'title', 'faultType','location','itemType', 'buttons'];
  
  constructor(
    private myFaultService: MyfaultService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit() {
    this.myFaultService.getFault().subscribe(faults=>{
      this.dataSource = faults.data;
      console.log(faults.data);
      
    })

    this.myFaultService.getFaultArcive().subscribe(faults=>{
      this.adataSource = faults.data;
    })
  }

  newFault():void{
    
  }

}

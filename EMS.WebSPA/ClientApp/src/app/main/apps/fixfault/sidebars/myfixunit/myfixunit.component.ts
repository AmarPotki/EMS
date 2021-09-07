import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { MatDialog } from '@angular/material';
import { FixUnitDto } from 'app/main/apps/fixunit/models/fixunit.model';
import { FixfaultService } from '../../fixfault.service';

@Component({
  selector: 'app-myfixunit',
  templateUrl: './myfixunit.component.html',
  styleUrls: ['./myfixunit.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class MyfixunitComponent implements OnInit {

  myfixunits: FixUnitDto[] = null;
  filters: any[];
  labels: any[];
  accounts: object;
  selectedAccount: string;
  dialogRef: any;

  constructor(
    public _matDialog: MatDialog,
    private fixfaultService : FixfaultService
  ) { }

  ngOnInit() {
    //this.myfixunits  = new FixUnitDto();
    this.fixfaultService.getMyfixUnit().subscribe(result=>{
      
      
      this.myfixunits = result
      console.log(this.myfixunits);
    })
  }

}

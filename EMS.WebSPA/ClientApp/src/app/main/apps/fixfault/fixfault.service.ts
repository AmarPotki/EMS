import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Guid } from '../../../../guid';
import { FixUnitDto } from '../fixunit/models/fixunit.model';
import { FaultListDto } from '../myfault/myfault.model';
import { FixFaultDto } from './fixfault.model';

let httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
};



@Injectable({
  providedIn: 'root'
})
export class FixfaultService {

  fixfaults: FixUnitDto[];

  onFixFaultsChanged: BehaviorSubject<any> = new BehaviorSubject([]);
  CurrentFixfault = this.onFixFaultsChanged.asObservable();

  private baseUrl = "https://localhost:44392";
  constructor(
    private _httpClient: HttpClient
  ) { 
  }

  getMyfixUnit(): Observable<FixUnitDto[]>{
    return this._httpClient.get<FixUnitDto[]>(`${this.baseUrl}/api/FixUnit/getMyFixUnits`);
  }

  getMyFault(fixUnitId:number, pageSize:number, pageIndex:number):Observable<FaultListDto>{
    return this._httpClient.get<FaultListDto>(`${this.baseUrl}/api/FixUnit/getFaults?fixUnitId=${fixUnitId}&pageSize=${pageSize}&pageIndex=${pageIndex}`);
  }

  setCurrentFixfault(fixfault: FixFaultDto):void{
    this.onFixFaultsChanged.next(fixfault)
  }
}


import { Injectable } from '@angular/core';
import { Guid } from '../../../../../guid';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { FaultTypeDto } from './fulttype.model';




@Injectable({
  providedIn: 'root'
})
export class FaulttypeService {
  private baseUrl = "https://localhost:44392";
  constructor(private _httpClient: HttpClient) { }

  getFaultType():Observable<FaultTypeDto[]>{
    return this._httpClient.get<FaultTypeDto[]>(`${this.baseUrl}/api/Admin/getFaultTypes`);
  }

  createFaultType(faulttype:FaultTypeDto):Observable<FaultTypeDto>{
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
    };
    return this._httpClient.post<FaultTypeDto>(`${this.baseUrl}/api/Admin/createFaultType`,faulttype,httpOptions);
  }

  updatefaultType(faulttype:FaultTypeDto):Observable<FaultTypeDto>{
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
    };
    return this._httpClient.post<FaultTypeDto>(`${this.baseUrl}/api/Admin/editFaultType`,faulttype,httpOptions);
  }

  deleteFaultType(faulttype:FaultTypeDto):Observable<any>{
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
    };
    return this._httpClient.post<any>(`${this.baseUrl}/api/Admin/deleteFaultType`,faulttype,httpOptions);
  }
  
}

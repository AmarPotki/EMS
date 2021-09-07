import { Injectable } from '@angular/core';
import { Guid } from '../../../../guid';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { FaultDto, AddFaultDto, FaultListDto } from './myfault.model';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MyfaultService {
  private baseUrl = "https://localhost:44392";
  constructor(
    private _httpClient: HttpClient
    ) { }
   
  createFault(fault:AddFaultDto):Observable<any>{
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
    };
    return this._httpClient.post<any>(`${this.baseUrl}/api/Fault/addFault`,fault,httpOptions)
    .pipe(
      catchError(this.handleError)
    );
  }

  getFault():Observable<FaultListDto>{
    return this._httpClient.get<FaultListDto>(`${this.baseUrl}/api/Fault/myFaults`)
    .pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  getFaultArcive():Observable<FaultListDto>{
    return this._httpClient.get<FaultListDto>(`${this.baseUrl}/api/Fault/myArchiveFaults`)
    .pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  getFaultDetail(Id:number):Observable<any>{
    return this._httpClient.get<any>(`${this.baseUrl}/pi/Fault/getFault/${Id}`)
    .pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  handleError(error:any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `خطا: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `کد خطا: ${error.status}\nپیام خطا: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}

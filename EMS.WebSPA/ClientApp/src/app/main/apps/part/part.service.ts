import { Injectable } from '@angular/core';
import { Guid } from '../../../../guid';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { PartsDto } from './parts.model';

let httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': Guid.newGuid() })
};


@Injectable({
  providedIn: 'root'
})
export class PartService {
  private baseUrl = "https://localhost:44392";
  constructor(private _httpClient: HttpClient) {
  }
    
  getParts():Observable<PartsDto[]>{
    return this._httpClient.get<PartsDto[]>(`${this.baseUrl}/api/Admin/getParts`);
  }

  createPart(part:PartsDto):Observable<PartsDto>{
    return this._httpClient.post<PartsDto>(`${this.baseUrl}/api/Admin/createPart`,part,httpOptions);
  }

  updatePart(part:PartsDto):Observable<PartsDto>{
    return this._httpClient.post<PartsDto>(`${this.baseUrl}/api/Admin/editPart`,part,httpOptions);
  }

  deletePart(part:PartsDto):Observable<any>{
    return this._httpClient.post<any>(`${this.baseUrl}/api/Admin/deletePart`,part,httpOptions);
  }
   
}

import { Injectable } from '@angular/core';
import { Guid } from '../../../../../guid';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigurationService } from 'app/shared/services/configuration.service';
import { FixUnitDto } from '../models/fixunit.model';
import { UserDto } from '../../usermanager/usermanager.model';
import { Member } from '../models/fix-unit-member';

@Injectable({
  providedIn: 'root'
})
export class FixUnitService {
  fixUnitBaseUrl = "https://localhost:44392/api/FixUnit";
  adminBaseUrl = "https://localhost:44392/api/Admin";
  constructor(
    private _httpClient: HttpClient,
    private configurationService: ConfigurationService,
  ) {
    this.configurationService.fetch();
  }

  createFixunit(fixunit: FixUnitDto): Observable<FixUnitDto> {
    let guid = Guid.newGuid();
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': guid })
    };
    console.log("this.configurationService.createFixUnitUrl", this.configurationService.createFixUnitUrl);
    return this._httpClient.post<FixUnitDto>(this.configurationService.createFixUnitUrl, fixunit, httpOptions);
  }

  deleteFixUnit(fixUnitId: number): Observable<Object> {
    let guid = Guid.newGuid();
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': guid })
    };
    console.log("this.configurationService.deleteFixUnitUrl", this.configurationService.deleteFixUnitUrl);
    return this._httpClient.post(this.configurationService.deleteFixUnitUrl, { id: fixUnitId }, httpOptions);
  }

  updateFixUnit(fixUnit: FixUnitDto) {
    let guid = Guid.newGuid();
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': guid })
    };
    return this._httpClient.post<FixUnitDto>(`${this.adminBaseUrl}/editFixUnit`, fixUnit, httpOptions);
  }

  getFixUnits(): Observable<FixUnitDto[]> {
    return this._httpClient.get<FixUnitDto[]>(`${this.fixUnitBaseUrl}/getFixUnits`);
  }

  getFixUnit(fixUnitId: number): Observable<FixUnitDto> {
    return this._httpClient.get<FixUnitDto>(`${this.fixUnitBaseUrl}/getFixUnit/${fixUnitId}`);
  }

  getFixUnitMembers(fixUnitId: number): Observable<UserDto[]> {
    return this._httpClient.get<UserDto[]>(`${this.fixUnitBaseUrl}/getMembers?fixUnitId=${fixUnitId}`);
  }

  removeFixUnitMember(data: Member) {
    let guid = Guid.newGuid();
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': guid })
    };
    return this._httpClient.post<FixUnitDto>(`${this.adminBaseUrl}/removeMember`, data, httpOptions);
  }

  addFixUnitMember(data: Member) {
    let guid = Guid.newGuid();
    let httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'x-requestid': guid })
    };
    return this._httpClient.post<FixUnitDto>(`${this.adminBaseUrl}/addMember`, data, httpOptions);
  }
}
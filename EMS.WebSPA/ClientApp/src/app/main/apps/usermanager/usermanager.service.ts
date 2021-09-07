import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDto, CreateUserDto } from './usermanager.model';
import { Guid } from '../../../../guid';
import { ConfigurationService } from 'app/shared/services/configuration.service';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';

let httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class UsermanagerService {

  private baseUrl = "https://localhost:44392";
  constructor(
    private _httpClient: HttpClient,
    private configurationService: ConfigurationService
  ) {
    //httpOptions.headers.append('accept', 'application/json');
    this.configurationService.fetch();
  }

  

  searchUsers(user: string) {
    console.log("user in api", user);
    return this._httpClient.post<UserDto[]>(`${this.baseUrl}/api/User/searchUser`, user).pipe(
      debounceTime(300),
      distinctUntilChanged(),
    )
  }

  getUsers(): Observable<UserDto[]>{
    return this._httpClient.get<UserDto[]>(`${this.baseUrl}/api/User/getUsers`);
  }

  newUser(user: CreateUserDto): Observable<UserDto> {
    httpOptions.headers.append('x-requestid', Guid.newGuid());
    return this._httpClient.post<UserDto>(`${this.baseUrl}/api/Admin/createUser`, user, httpOptions);
  }

  getUserforEdit(userId: string): Observable<UserDto> {
    return this._httpClient.get<UserDto>(`${this.baseUrl}/api/Admin/getUser?userIdentity=${userId}`);
  }

  resetPassword(userId: string): Observable<UserDto> {
    let uIdentity = {
      userIdentity: userId
    }
    //httpOptions.headers.append('X-Request-ID', Guid.newGuid());
    return this._httpClient.post<UserDto>(`${this.baseUrl}/api/Admin/resetPassword`, uIdentity, httpOptions);
  }

  lockUser(userId: string): Observable<UserDto> {
    let uIdentity = {
      userIdentity: userId
    }
    console.log(httpOptions);

    //httpOptions.headers.append('X-Request-ID', guid);
    return this._httpClient.post<UserDto>(`${this.baseUrl}/api/Admin/lockOutUser`, uIdentity, httpOptions);
  }

  unlockUser(userId: string): Observable<UserDto> {
    let uIdentity = {
      userIdentity: userId
    }

    //httpOptions.headers.append('X-Request-ID', Guid.newGuid());
    return this._httpClient.post<UserDto>(`${this.baseUrl}/api/Admin/unLockOutUser`, uIdentity, httpOptions);
  }

}

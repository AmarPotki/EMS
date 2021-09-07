import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tree } from '../models/tree.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Guid } from 'guid';
let httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'accept': 'application/json',
   })
};


@Injectable({
  providedIn: 'root'
})
export class TreeService {
  constructor(
    private http: HttpClient
    // private httpService:DataService
  ) { }

  public getAllNodes(url: string): Observable<Tree[]> {
    return this.http.get<Tree[]>(url,httpOptions);
  }
  
  public getNodeChild(url: string,parentId:number): Observable<Tree[]> {
    return this.http.get<Tree[]>(`${url}/${parentId}`,httpOptions);
  }
  
  public AddNode( url: string,data): Observable<object> {
    let guid = Guid.newGuid();
    return this.http.post<Tree>(url, data,
      {
        headers: { 'x-requestid': guid }
      }
    );
  }

  public deleteNode(url: string, nodeId: number) {
    console.log(nodeId);
    console.log(url);
    let guid = Guid.newGuid();
    return this.http.post<any>(url, { id: nodeId },
      {
        headers: { 'x-requestid': guid }
      }
    );
  }

  public updateNode(url: string, node: any): Observable<object> {
    let guid = Guid.newGuid();
    return this.http.post<any>(url, node,
      {
        headers: { 'x-requestid': guid }
      }
    );
  }

  public disableNode(url: string, nodeId: number): Observable<object> {
    let guid = Guid.newGuid();
    return this.http.post<any>(url, { id: nodeId },
      {
        headers: { 'x-requestid': guid }
      }
    );
  }

  public enableNode(url: string, nodeId: number): Observable<object> {
    let guid = Guid.newGuid();
    return this.http.post<any>(url, { id: nodeId },
      {
        headers: { 'x-requestid': guid }
      }
    );
  }

}






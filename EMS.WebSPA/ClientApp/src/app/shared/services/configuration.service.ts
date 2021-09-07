import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';
import { ApiUrls } from '../configuration/api-urls';
import { Subject } from 'rxjs';
import { IConfiguration } from '../models/configuration.model';


@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  public createLocationUrl: string;
  public updateLocationUrl: string;
  public deleteLocationUrl: string;
  public getAllLocationsUrl: string;
  public getLocationsUrl: string;
  public createItemTypeUrl: string;
  public updateItemTypeUrl: string;
  public deleteItemTypeUrl: string;
  public getAllItemTypesUrl: string;
  public getItemTypeUrl: string;
  public createFixUnitUrl: string;
  public getFixUnitUrl: string;
  public getGetUsersUrl: string;
  public deleteFixUnitUrl: string;
  public editFixUnitUrl: string

  serverSettings: IConfiguration;
  private settingsLoadedSource = new Subject();
  settingsLoaded$ = this.settingsLoadedSource.asObservable();
  isReady = false;

  constructor(
    public treeApiUrls: ApiUrls,
    private storageService: StorageService) { }
  public load() {
    const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
      this.serverSettings = { adminController: `${baseURI}/api/admin`, identityUrl: 'https://localhost:44392/' };
    this.storageService.store('identityUrl', this.serverSettings.identityUrl);
    this.storageService.store('adminController', this.serverSettings.adminController);
    this.isReady = true;
    this.settingsLoadedSource.next();


    let apiUrls = this.treeApiUrls.apiUrls;
    this.storageService.store('createLocation', apiUrls.createLocation);
    this.storageService.store('updateLocation', apiUrls.updateLocation);
    this.storageService.store('deleteLocation', apiUrls.deleteLocation);
    this.storageService.store('getAllLocations', apiUrls.getAllLocations);
    this.storageService.store('getLocations', apiUrls.getLocations);
    this.storageService.store('createItemType', apiUrls.createItemType);
    this.storageService.store('updateItemType', apiUrls.updateItemType);
    this.storageService.store('deleteItemType', apiUrls.deleteItemType);
    this.storageService.store('getAllItemTypes', apiUrls.getAllItemTypes);
    this.storageService.store('getItemType', apiUrls.getItemType);
    this.storageService.store('getFixUnit', apiUrls.getFixUnits);
    this.storageService.store('getGetUsersUrl', apiUrls.getUsers);
    this.storageService.store('createFixUnit', apiUrls.createFixUnit);
    this.storageService.store('deleteFixUnit', apiUrls.deleteFixUnit);
    this.storageService.store('editFixUnit', apiUrls.editFixUnit);
    this.storageService.store('getFixUnitUrl', apiUrls.getFixUnits);
 
  }

  public fetch() {
    this.createLocationUrl = this.storageService.retrieve('createLocation'),
      this.updateLocationUrl = this.storageService.retrieve('updateLocation'),
      this.deleteLocationUrl = this.storageService.retrieve('deleteLocation'),
      this.getAllLocationsUrl = this.storageService.retrieve('getAllLocations'),
      this.getLocationsUrl = this.storageService.retrieve('getLocations'),
      this.createItemTypeUrl = this.storageService.retrieve('createItemType'),
      this.updateItemTypeUrl = this.storageService.retrieve('updateItemType'),
      this.deleteItemTypeUrl = this.storageService.retrieve('deleteItemType'),
      this.getAllItemTypesUrl = this.storageService.retrieve('getAllItemTypes'),
      this.getItemTypeUrl = this.storageService.retrieve('getItemType'),
      this.createFixUnitUrl = this.storageService.retrieve('createFixUnit'),
      this.getFixUnitUrl = this.storageService.retrieve('getFixUnits'),
      // this.getGetUsersUrl = this.storageService.retrieve('getUsers')
      this.getGetUsersUrl = this.storageService.retrieve('getGetUsersUrl'),
      this.deleteFixUnitUrl = this.storageService.retrieve('deleteFixUnit'),
      this.editFixUnitUrl = this.storageService.retrieve('editFixUnit')
 
      this.getFixUnitUrl = this.storageService.retrieve('getFixUnits')

  }

}

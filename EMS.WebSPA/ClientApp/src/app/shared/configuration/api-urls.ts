import { Injectable } from '@angular/core';
import { ApiUrlsModel } from '../models/api-urls.model';
   
@Injectable({
    providedIn: 'root'
})
export class ApiUrls {
    public apiUrls = new ApiUrlsModel(
        'https://localhost:44392/api/Admin/createLocation', //createLocation apiUrl
        'https://localhost:44392/api/Admin/updateLocation', //updateLocation  apiUrl
        'https://localhost:44392/api/Admin/deleteLocation', //deleteLocation  apiUrl
        'https://localhost:44392/api/Admin/getAllLocations', //getAllLocations  apiUrl
        'https://localhost:44392/api/Admin/getLocations', //getLocations  apiUrl
        'https://localhost:44392/api/Admin/createItemType', //createItemType  apiUrl
        'https://localhost:44392/api/Admin/editItemType', //updateItemType  apiUrl
        'https://localhost:44392/api/Admin/deleteItemType', //deleteItemType  apiUrl
        'https://localhost:44392/api/Admin/getAllItemTypes', //getAllItemType  apiUrl
        'https://localhost:44392/api/Admin/getItemTypes', //getItemType  apiUrl
        'https://localhost:44392/api/Admin/createFixUnit', //createFixUnit  apiUrl
        'https://localhost:44392/api/FixUnit/getFixUnits', //getFixUnits  apiUrl
        'https://localhost:44392/api/User/getUsers', //getFixUnits  apiUrl
        'https://localhost:44392/api/Admin/deleteFixUnit',
        'https://localhost:44392/api/Admin/editFixUnit'
    )
}
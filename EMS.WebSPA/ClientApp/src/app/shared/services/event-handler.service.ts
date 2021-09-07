import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ActionsFormData } from '../models/actoins-form-data';

@Injectable({
  providedIn: 'root'
})
export class EventHandlerService {

  // start of  Location Observables
  private addLocationEventSource = new BehaviorSubject<string>('');
  public addLocationEvent$ = this.addLocationEventSource.asObservable();

  private updateLocationEventSource = new BehaviorSubject<string>('');
  public updateLocationEvent$ = this.updateLocationEventSource.asObservable();

  private removeLocationEventSource = new BehaviorSubject<boolean>(false);
  public removeLocationEvent$ = this.removeLocationEventSource.asObservable();

  private currentSelectedLocationNodeSource = new BehaviorSubject<any>(null);
  public currentSelectedLocationNode$ = this.currentSelectedLocationNodeSource.asObservable();

  private showLocationActionsButtonSectionSource = new BehaviorSubject<boolean>(true);
  public showLocationActionsButtonSection$ = this.showLocationActionsButtonSectionSource.asObservable();

  private closeLocationActionsFormSource = new BehaviorSubject<boolean>(true);
  public closeLocationActionsForm$ = this.closeLocationActionsFormSource.asObservable();

  private showLocationActionFormSource = new BehaviorSubject<ActionsFormData>(new ActionsFormData(0, null, false));
  public showLocationActionForm$ = this.showLocationActionFormSource.asObservable();
  // end of  Location Observables

  // start of  ItemType Observables
  private addItemTypeEventSource = new BehaviorSubject<string>('');
  public addItemTypeEvent$ = this.addItemTypeEventSource.asObservable();

  private updateItemTypeEventSource = new BehaviorSubject<string>('');
  public updateItemTypeEvent$ = this.updateItemTypeEventSource.asObservable();

  private removeItemTypeEventSource = new BehaviorSubject<boolean>(false);
  public removeItemTypeEvent$ = this.removeItemTypeEventSource.asObservable();

  private currentSelectedItemTypeNodeSource = new BehaviorSubject<any>(null);
  public currentSelectedItemTypeNode$ = this.currentSelectedItemTypeNodeSource.asObservable();

  private showItemTypeActionsButtonSectionSource = new BehaviorSubject<boolean>(true);
  public showItemTypeActionsButtonSection$ = this.showItemTypeActionsButtonSectionSource.asObservable();

  private closeItemTypeActionsFormSource = new BehaviorSubject<boolean>(true);
  public closeItemTypeActionsForm$ = this.closeItemTypeActionsFormSource.asObservable();

  private showItemTypeActionFormSource = new BehaviorSubject<ActionsFormData>(new ActionsFormData(0, null, false));
  public showItemTypeActionForm$ = this.showItemTypeActionFormSource.asObservable();
  // end of  ItemType Observables

  emitAddLocationEvent(nodeName: string) {
    this.addLocationEventSource.next(nodeName);
  }

  getEmittedAddLocationValue$() {
    return this.addLocationEvent$;
  }

  emitUpdateLocationEvent(nodeName: string) {
    this.updateLocationEventSource.next(nodeName);
  }
  getEmittedUpdateLocationValue$() {
    return this.updateLocationEvent$;
  }

  emitRemoveLocationEvent() {
    this.removeLocationEventSource.next(true);
  }

  getEmittedRemoveLocationValue$() {
    return this.removeLocationEvent$;
  }

  emitCurrentSelectedLocationNodeEvent(node: any) {
    this.currentSelectedLocationNodeSource.next(node);
  }
  getEmittedCurrentSelectedLocationNodeValue$() {
    return this.currentSelectedLocationNode$;
  }

  emitShowLocationActionsButtonSectionEvent(showSection: boolean) {
    this.showLocationActionsButtonSectionSource.next(showSection);
  }
  getEmittedShowLocationActionsButtonSectionValue$() {
    return this.showLocationActionsButtonSection$;
  }

  emitShowLocationActionFormEvent(actionFormData: ActionsFormData) {
    this.showLocationActionFormSource.next(actionFormData);
  }
  getEmittedShowLocationActionFormValue$() {
    return this.showLocationActionForm$;
  }


  emitAddItemTypeEvent(nodeName: string) {
    this.addItemTypeEventSource.next(nodeName);
  }

  getEmittedAddItemTypeValue$() {
    return this.addItemTypeEvent$;
  }

  emitUpdateItemTypeEvent(nodeName: string) {
    this.updateItemTypeEventSource.next(nodeName);
  }
  getEmittedUpdateItemTypeValue$() {
    return this.updateItemTypeEvent$;
  }

  emitRemoveItemTypeEvent() {
    this.removeItemTypeEventSource.next(true);
  }

  getEmittedRemoveItemTypeValue$() {
    return this.removeItemTypeEvent$;
  }

  emitCurrentSelectedItemTypeNodeEvent(node: any) {
    this.currentSelectedItemTypeNodeSource.next(node);
  }
  getEmittedCurrentSelectedItemTypeNodeValue$() {
    return this.currentSelectedItemTypeNode$;
  }

  emitShowItemTypeActionsButtonSectionEvent(showSection: boolean) {
    this.showItemTypeActionsButtonSectionSource.next(showSection);
  }
  getEmittedShowItemTypeActionsButtonSectionValue$() {
    return this.showItemTypeActionsButtonSection$;
  }

  emitShowItemTypeActionFormEvent(actionFormData: ActionsFormData) {
    this.showItemTypeActionFormSource.next(actionFormData);
  }
  getEmittedShowItemTypeActionFormValue$() {
    return this.showItemTypeActionForm$;
  }
}

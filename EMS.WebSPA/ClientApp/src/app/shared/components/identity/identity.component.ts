import { Component, OnInit, OnChanges, Output, Input, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';

import { IIdentity } from '../../models/identity.model';
import { SecurityService } from '../../services/security.service';

@Component({
  selector: 'esh-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.scss']
})
export class IdentityComponent implements OnInit {
  authenticated = false;
  private subscription: Subscription;
    private userName = '';
    private role = '';

  constructor(private service: SecurityService) {

  }

  ngOnInit(): void {
    this.subscription = this.service.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
        this.userName = this.service.UserData.email;
        this.role = this.service.UserData.role;
    });

    if (window.location.hash) {
      this.service.AuthorizedCallback();
    }

    console.log('identity component, checking authorized' + this.service.IsAuthorized);
    this.authenticated = this.service.IsAuthorized;

      if (this.authenticated) {
          if (this.service.UserData) {
              this.userName = this.service.UserData.email;
          }

      } else {
          this.service.Authorize();
      }
  }

  logoutClicked(event: any): void {
    event.preventDefault();
    console.log('Logout clicked');
    this.logout();
  }

  login(): void {
    this.service.Authorize();
  }

  logout(): void {
    this.service.Logoff();
  }
}
  



import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs';

import { NavPanelComponent } from '../nav-panel/nav-panel.component';
import { Settings } from '../../settings';
import { SessionService } from '../../services/session.service';

declare let gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {
  providerId: string;
  clientId: string;
  auth2: any;
  
  constructor(private http: HttpClient, private sessionService: SessionService) {
    this.providerId = Settings.PROVIDER_ID;
    this.clientId = Settings.CLIENT_ID;
  }

  ngOnInit() {
    this.loadScript(this.providerId,
      "//apis.google.com/js/platform.js",
      () => {
        gapi.load('auth2', () => {
          this.auth2 = gapi.auth2.init({
            client_id: this.clientId,
            scope: 'email'
          });
        });
    });
  }

  signInWithGoogle() {
    console.log("Sign in!");
    let promise = this.auth2.signIn();   
          promise.then(() => {          
            let authData = this.auth2.currentUser.get().getAuthResponse(true);
            console.log(authData.id_token);
            const url = `${Settings.API_ORIGIN_API}/api/login/externallogin?googleToken=${authData.id_token}`;
            this.http.get(url)
            .map(res => res as Login)
            .subscribe(data => {
              console.log(data);
              console.log(data.token);
              this.sessionService.setToken(data.token);
              this.sessionService.setAdmin(data.isAdmin);
              NavPanelComponent.updateUserLoginStatus.next(true);
            }); 
          });
  }

  private extractData(res: Response) {
    let body = res.json();
    console.log(body);
    return body;
} 

private handleErrorObservable (error: Response | any) {
  console.error(error.message || error);
  return Observable.throw(error.message || error);
} 

  loadScript(id: string, src: string, onload: any): void {
    if (document.getElementById(id)) { return; }
    let signInJS = document.createElement("script");
    signInJS.async = true;
    signInJS.src = src;
    signInJS.onload = onload;
    document.head.appendChild(signInJS);
}
}

  interface Login {
  requestAt: string;
  expiresIn: string;
  tokenType: string;
  token: string;
  isAdmin: string;
}


import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
declare let gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {
  PROVIDER_ID: string = "GOOGLE";
  clientId = "968867183125-6vik6skj25h8kpjosgf3a0ed953rt7r7.apps.googleusercontent.com";
  auth2: any;
  
  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.loadScript(this.PROVIDER_ID,
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
            this.http.get<Login>(`http://localhost:52230/api/login/externallogin?googleToken=${authData.id_token}`).subscribe(response => {
              console.log(response);
              console.log(response.token);
            });            
          });

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
  requestAt: Date;
  expiresIn: Date;
  tokenType: string;
  token: string;
  isAdmin: boolean;
}


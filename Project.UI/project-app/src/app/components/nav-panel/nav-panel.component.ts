import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Settings } from '../../settings';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-nav-panel',
  templateUrl: './nav-panel.component.html',
  styleUrls: ['./nav-panel.component.css']
})
export class NavPanelComponent implements OnInit {

  private settings: Settings;

  constructor(private httpClient: HttpClient, private router: Router, private sessionService: SessionService) { }

  ngOnInit() {
  }

  public isLoggedIn(): boolean {
    return this.sessionService.checkLogin();
  }

  public isAdmin(): boolean {
    return this.sessionService.checkAdmin();
  }

  public onLogin(): void {
    alert(Settings.API_ORIGIN_API);
    var jsonpCallback = '/';
    // this.httpClient.get<ILoginResult>(Settings.API_ORIGIN_API + '/api/login/external')
    // .subscribe(result => {
    //   this.sessionService.setAdmin(result.isAdmin);
    //   this.sessionService.setToken(result.token);
    // }); 
  }

  public onLogout(): void {
    this.sessionService.logout();
    this.router.navigate(['/']);
  }
}


interface ILoginResult {
  requestAt: string;
  expiresIn: string;
  tokenType: string;
  token: string;
  isAdmin: string;
}


class LoginResult implements ILoginResult {
  requestAt: string;
  expiresIn: string;
  tokenType: string;
  token: string;
  isAdmin: string;
}

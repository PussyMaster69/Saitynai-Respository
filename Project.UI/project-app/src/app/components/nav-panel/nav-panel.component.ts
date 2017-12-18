import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';


import { SessionService } from '../../services/session.service';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Component({
  selector: 'app-nav-panel',
  templateUrl: './nav-panel.component.html',
  styleUrls: ['./nav-panel.component.css']
})
export class NavPanelComponent implements OnInit {

  public static updateUserLoginStatus: Subject<boolean> = new Subject();

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
    // setup a listener
    NavPanelComponent.updateUserLoginStatus.subscribe(res => {
      console.log(res);
      if (res) {
        // this.router.navigate(['/login']);
      }
    });
    
  }

  public onLogout(): void {
    this.sessionService.logout();
    this.router.navigate(['/']);
  }
}

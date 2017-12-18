import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {MatIconRegistry} from '@angular/material'
import {DomSanitizer} from '@angular/platform-browser';



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

  public isLoggedIn = false;

  public static updateUserLoginStatus: Subject<boolean> = new Subject();

  constructor(
    private httpClient: HttpClient, 
    private router: Router, 
    private sessionService: SessionService, 
    public iconReg: MatIconRegistry,
    public sanitizer: DomSanitizer) { 
      iconReg.addSvgIcon(
        'hamburger',
        sanitizer.bypassSecurityTrustResourceUrl('../../../assets/Hamburger_icon.svg'));
    }

  ngOnInit() {
    if (this.IsLoggedIn()) {
      this.isLoggedIn = true;
    } else {
      this.isLoggedIn = false;
      this.router.navigate['/'];
    }
  }

  public IsLoggedIn(): boolean {
    console.log("check login");
    return this.sessionService.checkLogin();
  }

  public isAdmin(): boolean {
    return this.sessionService.checkAdmin();
  }

  public onLogin(): void {
    // setup a listener
    NavPanelComponent.updateUserLoginStatus.subscribe(res => {
      console.log(res);
      if (this.sessionService.checkLogin()) {
        this.isLoggedIn = res;
      }
    });
  }

  public onLogout(): void {
    this.isLoggedIn = false;
    this.sessionService.logout();
    this.router.navigate(['/']);
  }

  public openMenu(): void {

  }
}

import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private sessionService: SessionService, private router: Router) { }

  ngOnInit() {
    var isLoggedIn = this.sessionService.checkLogin();
    var isAdmin = this.sessionService.checkAdmin();
    if (!isLoggedIn || !isAdmin) {
      this.sessionService.logout();
      this.router.navigate(['/']);
    }
  }
}

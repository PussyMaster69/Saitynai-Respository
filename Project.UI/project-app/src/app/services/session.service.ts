import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';


@Injectable()
export class SessionService {

  private tokenKey = "token";
  private adminKey = "isAdmin";
  private token: string;

  constructor(private http: HttpClient) { }

  public setToken(token: string): void {
    sessionStorage.setItem(this.tokenKey, token);
  }

  public logout(): void {
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem(this.adminKey);
  }

  public setAdmin(admin: string): void {
    sessionStorage.setItem(this.adminKey, admin);
  }

  public checkLogin(): boolean {
    var token = sessionStorage.getItem(this.tokenKey);
    return token != null
  }

  public checkAdmin(): boolean {
    var admin = sessionStorage.getItem(this.adminKey);
    if (admin != null) {
      return admin == 'true';
    }
    return false;
  }

  public getLocalToken(): string {
    if (!this.token) {
      this.token = sessionStorage.getItem(this.tokenKey);
    }
    return this.token;
  } 
}

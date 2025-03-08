import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public loggedIn: boolean = false;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {

    const url = `${GLOBAL_CONFIG.apiBaseUrl}users/login`;
    const params = new HttpParams().set('usr', username).set('psw', password);
    console.log(url);
    return this.http.post(url, null, { params });
  }

  logout() {
    this.loggedIn = false;
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}

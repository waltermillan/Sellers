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
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Users/Auth';

    // Realizamos la llamada HTTP pasando los parámetros usr y psw
    const params = new HttpParams().set('usr', username).set('psw', password);
    console.log(url);
    return this.http.get(url, { params });
  }

  logout() {
    this.loggedIn = false;  // Marca como no autenticado
  }

  isLoggedIn() {
    return this.loggedIn;  // Verifica si el usuario está autenticado
  }
}

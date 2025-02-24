import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public loggedIn: boolean = false;
  private apiUrl: string = 'http://localhost:5184/api/User/Auth';  // La URL de tu API

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    // Realizamos la llamada HTTP pasando los parámetros usr y psw
    const params = new HttpParams().set('usr', username).set('psw', password);
    return this.http.get(this.apiUrl, { params });
  }

  logout() {
    this.loggedIn = false;  // Marca como no autenticado
  }

  isLoggedIn() {
    return this.loggedIn;  // Verifica si el usuario está autenticado
  }
}

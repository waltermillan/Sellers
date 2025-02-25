import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { GLOBAL_CONFIG } from '../config/config.global';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'users';
    return this.http.get<User[]>(url);
  }

  add(user:User){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'users';
    return this.http.post(url, user);
  }

  update(user:User, id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'users/' + id;
    return this.http.put(url, user);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'users/' + id;
    return this.http.delete(url);
  }
}

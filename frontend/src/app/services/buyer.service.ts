import { Injectable } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { HttpClient } from '@angular/common/http';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class BuyerService {

  constructor(private http: HttpClient) { }//http://localhost:5184/api/buyers

  getAll(){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'buyers';
    return this.http.get<Buyer[]>(url);
  }

  add(buyer:Buyer){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'buyers';
    return this.http.post(url, buyer);
  }

  update(buyer:Buyer, id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'buyers/ + id';
    return this.http.put(url, buyer);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'buyers/' + id;
    return this.http.delete(url);
  }
}

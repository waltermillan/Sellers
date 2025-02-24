import { Injectable } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { HttpClient } from '@angular/common/http';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class BuyerService {

  constructor(private http: HttpClient) { }

  getAll(){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Buyer/GetAll';
    return this.http.get<Buyer[]>(url);
  }

  add(buyer:Buyer){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Buyer/Add';
    return this.http.post(url, buyer);
  }

  update(buyer:Buyer){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Buyer/Update';
    return this.http.put(url, buyer);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Buyer/Delete?id=' + id;
    return this.http.delete(url);
  }
}

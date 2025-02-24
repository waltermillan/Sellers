import { Injectable } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BuyerService {

  constructor(private http: HttpClient) { }

  getAll(){
    const url = 'http://localhost:5184/api/Buyer/GetAll';
    return this.http.get<Buyer[]>(url);
  }

  add(buyer:Buyer){
    const url = 'http://localhost:5184/api/Buyer/Add';
    return this.http.post(url, buyer);
  }

  update(buyer:Buyer){
    const url = 'http://localhost:5184/api/Buyer/Update';
    return this.http.put(url, buyer);
  }

  delete(id:number){
    const url = 'http://localhost:5184/api/Buyer/Delete?id=' + id;
    return this.http.delete(url);
  }
}

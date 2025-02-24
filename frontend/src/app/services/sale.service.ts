import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Sale } from '../models/sale.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  
  constructor(private http: HttpClient) { }

  //Get all Sales
  getAll():Observable<Sale[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Sale/GetAll' ;
    console.log(url);
    return this.http.get<Sale[]>(url);
  }

  //Add new Sale
  add(sale:Sale){

    const url = GLOBAL_CONFIG.apiBaseUrl + 'Sale/Add' ;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    console.log(JSON.stringify(sale));
    return this.http.post(url, sale, { headers });
  }

  //Update a Sale
  update(sale:Sale){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Sale/Update';
    return this.http.put(url, sale);
  }

  //Delete a Sale
  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Sale/Delete?id=' + id;
    return this.http.delete(url);
  }
}

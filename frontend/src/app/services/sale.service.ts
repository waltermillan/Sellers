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

  getAll():Observable<Sale[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales' ;
    console.log(url);
    return this.http.get<Sale[]>(url);
  }

  add(sale:Sale){

    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales' ;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    console.log(JSON.stringify(sale));
    return this.http.post(url, sale, { headers });
  }

  update(sale:Sale, id: number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/' + id;
    return this.http.put(url, sale);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/' + id;
    return this.http.delete(url);
  }
}

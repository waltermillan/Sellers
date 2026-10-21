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

  add(sale: Sale) {
      const url = GLOBAL_CONFIG.apiBaseUrl + 'sales';

      const headers = new HttpHeaders({
          'Content-Type': 'application/json'
      });

      const [dia, mes, anio] = sale.date.split('/');

      const saleToSend = {
          ...sale,
          date: new Date(
              Number(anio),
              Number(mes) - 1,
              Number(dia)
          ).toISOString()
      };

      console.log(JSON.stringify(saleToSend));

      return this.http.post(url, saleToSend, { headers });
  }

  addBulk() {
      const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed/bulk';
      alert('url: ' + url);
      return this.http.post(url, null);
  }

  update(sale:Sale, id: number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/' + id;
    return this.http.put(url, sale);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/' + id;
    return this.http.delete(url);
  }

  deleteBulk() {
    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/bulk';
    return this.http.delete(url);
  }
}

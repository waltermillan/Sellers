import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SaleHistoric } from '../models/saleHistoric.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class SaleHistoricService {
  
  constructor(private http: HttpClient) { }

  getAll():Observable<SaleHistoric[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesHistoric' ;
    console.log(url);
    return this.http.get<SaleHistoric[]>(url);
  }

  
add(saleHistoric: SaleHistoric) {
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesHistoric';

    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    console.log(JSON.stringify(saleHistoric));

    return this.http.post(url, saleHistoric, { headers });
}


  update(saleHistoric:SaleHistoric, id: number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesHistoric/' + id;
    return this.http.put(url, saleHistoric);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesHistoric/' + id;
    return this.http.delete(url);
  }
}
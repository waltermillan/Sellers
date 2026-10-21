import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SaleProcessed } from '../models/saleProcessed.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class SaleProcessedService {
  
  constructor(private http: HttpClient) { }

  getAll():Observable<SaleProcessed[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed' ;
    console.log(url);
    return this.http.get<SaleProcessed[]>(url);
  }

  
add(saleProcessed: SaleProcessed) {
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed';

    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    const [dia, mes, anio] = saleProcessed.date.split('/');

    const saleProcessedToSend = {
        ...saleProcessed,
        date: new Date(
            Number(anio),
            Number(mes) - 1,
            Number(dia)
        ).toISOString()
    };

    console.log(JSON.stringify(saleProcessedToSend));

    return this.http.post(url, saleProcessedToSend, { headers });
}

addBulk() {
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed/bulk';
    return this.http.post(url, null);
}


  update(saleProcessed:SaleProcessed, id: number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed/' + id;
    return this.http.put(url, saleProcessed);
  }

  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'salesprocessed/' + id;
    return this.http.delete(url);
  }
}

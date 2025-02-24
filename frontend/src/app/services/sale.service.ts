import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Sale } from '../models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  sales:Sale[] = [];
  urlGetAll = 'http://localhost:5184/api/Sale/GetAll';
  urlAddSale= 'http://localhost:5184/api/Sale/Add';




  constructor(private http: HttpClient) { }

  //Get all Sales
  getAll():Observable<Sale[]>{
    console.log(this.urlGetAll);
    return this.http.get<Sale[]>(this.urlGetAll);
  }

  //Add new Sale
  add(sale:Sale){

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    console.log(JSON.stringify(sale));
    return this.http.post(this.urlAddSale, sale, { headers });
  }

  //Update a Sale
  update(sale:Sale){
    const url = 'http://localhost:5184/api/Sale/Update';
    return this.http.put(url, sale);
  }

  //Delete a Sale
  delete(id:number){
    const url = 'http://localhost:5184/api/Sale/Delete?id=' + id;
    return this.http.delete(url);
  }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SaleDTO } from '../models/saleDTO.model';

@Injectable({
  providedIn: 'root'
})
export class SaleDTOService {

  sales:SaleDTO[] = [];
  urlGetAll = 'http://localhost:5184/api/SaleDTO/GetAll';
  urlAddSale= 'http://localhost:5184/api/Sale/Add';

  constructor(private http: HttpClient) { }

  //Get all Sales
  getAll():Observable<SaleDTO[]>{
    console.log(this.urlGetAll);
    return this.http.get<SaleDTO[]>(this.urlGetAll);
  }
}

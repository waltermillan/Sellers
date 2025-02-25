import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SaleDTO } from '../models/saleDTO.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class SaleDTOService {

  sales:SaleDTO[] = [];

  constructor(private http: HttpClient) { }

  //Get all Sales
  getAll():Observable<SaleDTO[]>{

    const url = GLOBAL_CONFIG.apiBaseUrl + 'saleDTOs';
    console.log(url);    
    return this.http.get<SaleDTO[]>(url);
  }
}

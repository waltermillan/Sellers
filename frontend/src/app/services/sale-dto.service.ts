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

  getAll():Observable<SaleDTO[]>{

    const url = GLOBAL_CONFIG.apiBaseUrl + 'sales/dto';
    return this.http.get<SaleDTO[]>(url);
  }
}

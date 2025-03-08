import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Seller } from '../models/seller.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class SellerService {
  
  constructor(private http: HttpClient) { }

  getAllSellers(): Observable<Seller[]>{
      const url = GLOBAL_CONFIG.apiBaseUrl + 'sellers';
      return this.http.get<Seller[]>(url);
  }

  addSeller(seller: Seller): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers'
    return this.http.post<Seller>(url, seller);
  }

  updateSeller(seller: Seller, id: number): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers/' + id;
    return this.http.put<Seller>(url, seller);
  }
  
  deleteSeller(id: number): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers/' + id;
    return this.http.delete<Seller>(url);
  }
}

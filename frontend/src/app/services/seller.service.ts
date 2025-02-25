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

  // Método para obtener todos los sellers
  getAllSellers(): Observable<Seller[]>{
      const url = GLOBAL_CONFIG.apiBaseUrl + 'sellers';
      //console.log(this.url);
      return this.http.get<Seller[]>(url);
  }

  // Método para crear un nuevo Seller
  addSeller(seller: Seller): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers'
    //console.log("->addSeller");
    return this.http.post<Seller>(url, seller);
  }

  // Método para actualizar un Seller
  updateSeller(seller: Seller, id: number): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers/' + id;
    //console.log("->updateSeller");
    return this.http.put<Seller>(url, seller);
  }
  

  // Método para actualizar un Seller
  deleteSeller(id: number): Observable<Seller> {
    const url  = GLOBAL_CONFIG.apiBaseUrl + 'sellers/' + id;
    return this.http.delete<Seller>(url);
  }
}

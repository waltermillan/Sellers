import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Seller } from '../models/seller.model';

@Injectable({
  providedIn: 'root'
})
export class SellerService {

  private api:string = 'http://localhost:5184/api/Seller/';//ttp://localhost:5184/api/Seller/Update
  url:string = '';
  
  constructor(private http: HttpClient) { }

  // Método para obtener todos los sellers
  getAllSellers(): Observable<Seller[]>{
      this.url = this.api + 'GetAll';
      //console.log(this.url);
      return this.http.get<Seller[]>(this.url);
  }

  // Método para crear un nuevo Seller
  addSeller(seller: Seller): Observable<Seller> {
    this.url = this.api + 'Add'
    //console.log("->addSeller");
    return this.http.post<Seller>(this.url, seller);
  }

  // Método para actualizar un Seller
  updateSeller(seller: Seller): Observable<Seller> {
    this.url = this.api + 'Update';
    //console.log("->updateSeller");
    return this.http.put<Seller>(this.url, seller);
  }
  

  // Método para actualizar un Seller
  deleteSeller(id: number): Observable<Seller> {
    this.url = this.api + 'Delete?id=' + id;
    return this.http.delete<Seller>(this.url);
  }
}

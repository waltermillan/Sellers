import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product.model';
import { GLOBAL_CONFIG  } from '../config/config.global';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  products:Product[] = [];

  constructor(private http: HttpClient) { }

  //Get all Products
  getAll():Observable<Product[]>{
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Product/GetAll';
    return this.http.get<Product[]>(url);
  }

  //Add new product
  add(product:Product){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Product/Add';
    return this.http.post(url, product);
  }

  //Update a product
  update(product:Product){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Product/Update';
    return this.http.put(url, product);
  }

  //Delete a product
  delete(id:number){
    const url = GLOBAL_CONFIG.apiBaseUrl + 'Product/Delete?id=' + id;
    return this.http.delete(url);
  }
}

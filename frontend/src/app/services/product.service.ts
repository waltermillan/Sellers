import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  products:Product[] = [];
  urlGetAll = 'http://localhost:5184/api/Product/GetAll';
  urlAddProduct = 'http://localhost:5184/api/Product/Add';

  constructor(private http: HttpClient) { }

  //Get all Products
  getAll():Observable<Product[]>{
    console.log(this.urlGetAll);
    return this.http.get<Product[]>(this.urlGetAll);
  }

  //Add new product
  add(product:Product){
    return this.http.post(this.urlAddProduct, product);
  }

  //Update a product
  update(product:Product){
    const url = 'http://localhost:5184/api/Product/Update';
    return this.http.put(url, product);
  }

  //Delete a product
  delete(id:number){
    const url = 'http://localhost:5184/api/Product/' + 'Delete?id=' + id;
    return this.http.delete(url);
  }
}

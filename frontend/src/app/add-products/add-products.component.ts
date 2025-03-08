import { Component, OnInit } from '@angular/core';
import { Product  } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { FormGroup } from '@angular/forms';

import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-add-products',
  templateUrl: './add-products.component.html',
  styleUrl: './add-products.component.css'
})
export class AddProductsComponent implements OnInit {

  productForm!: FormGroup;
  
  newProduct : Product = {
    id: 0,
    name: "",
    packagingDate: null,
    price: 1,
    stock: 1
  };

  constructor(private productService : ProductService,
              public messageService:MessageService)
  {}

  ngOnInit(){

  }

  obSubmit(){
    this.addProduct()
  }

  addProduct():void{
    this.productService.add(this.newProduct).subscribe({
      next: (data) =>{
        this.messageService.showMessage = true;
        this.messageService.message = 'Product addedd successful';
        console.log('Product addedd successful: ' + JSON.stringify( this.newProduct));
      },
      error: (error) => {
        console.error("error adding product.");
        this.messageService.showMessage = true;
        this.messageService.message = 'error creating product.';
      }
    });
  }

  closeMessage(): void {
    this.messageService.showMessage = false;
  }

}

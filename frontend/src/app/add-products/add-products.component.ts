import { Component, OnInit } from '@angular/core';
import { Product  } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { FormBuilder, FormGroup } from '@angular/forms';

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
              public messageService:MessageService) {
    
  }

  ngOnInit(){

  }

  obSubmit(){
    this.add()
  }


  add():void{
    this.productService.add(this.newProduct).subscribe({
      next: (data) =>{
        this.messageService.showMessage = true;
        this.messageService.message = 'successful product registration';
        console.log('alta producto efectuada: ' + JSON.stringify( this.newProduct));
      },
      error: (error) => {
        console.error("error al crear un nuevo producto.");
        this.messageService.showMessage = true;
        this.messageService.message = 'error when creating a new product.';
      }
    });
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

}

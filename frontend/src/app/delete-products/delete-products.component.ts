import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-delete-products',
  templateUrl: './delete-products.component.html',
  styleUrl: './delete-products.component.css'
})
export class DeleteProductsComponent implements OnInit {

  products:Product[] = [];

  constructor(private productService:ProductService,
              public messageService:MessageService
  ) {

  }

  ngOnInit(){
    this.getAll();
  }

  getAll(){
    this.productService.getAll().subscribe({
      next: (data:Product[]) => {
        this.products = data;
      },
      error: (error) => {
        console.error('Error al cargar productos.');
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing products. Please try again.'
      }
    })
  }

  deleteProduct(id:number){
    this.productService.delete(id).subscribe({
      next:(data) => {
        console.log('Producto borrado.');
        this.messageService.showMessage = true;
        this.messageService.message = 'Producto borrado.';
        this.getAll();
      },
      error: (error) => {
        console.error('Error al borrar producto.')
      }
    });
  }

    // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

}

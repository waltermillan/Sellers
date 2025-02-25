import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-products',
  templateUrl: './update-products.component.html',
  styleUrl: './update-products.component.css'
})
export class UpdateProductsComponent implements OnInit {

  selectedProductId:number | null = null;
  products: Product[] = [];
  showMessage:boolean = false;
  message: string = '';

  productForm!: FormGroup; // Formulario reactivo

  updProduct: Product = {
    id: 0,
    name: '',
    packagingDate: null,
    stock: 1,
    price: 1
  }

  constructor(private productService: ProductService,
                private fb: FormBuilder) {

  }

 // Maneja el cambio de selección
 onProductChange(): void {
  if (this.selectedProductId != null) {
    //console.log('Vendedor encontrado:', this.selectedSellerId);
    const selectedSeller = this.products.find(s => s.id == this.selectedProductId);

    //console.log('Vendedor encontrado:', selectedSeller);

    if (selectedSeller) {
      this.updProduct.id = selectedSeller.id;
      this.updProduct.name = selectedSeller.name;
      this.updProduct.price = selectedSeller.price;
      this.updProduct.stock = selectedSeller.stock;
      
      // Convertimos la fecha a un objeto Date si es un string
      if (typeof selectedSeller.packagingDate === 'string') {
        this.updProduct.packagingDate = new Date(selectedSeller.packagingDate);
      } else {
        this.updProduct.packagingDate = selectedSeller.packagingDate;
      }

      // Convertir la fecha al formato 'yyyy-MM-dd' para el input
      const formattedDate = this.updProduct.packagingDate?.toISOString().split('T')[0];
      this.productForm.controls['packagingDate'].setValue(formattedDate);  // Asignamos al formulario en formato 'yyyy-MM-dd'
      } 
      else {
        console.warn('Vendedor no encontrado');
      }
    }
  }
  

  get packagingDate(): string {
    // Si 'birthday' es un objeto Date, lo convertimos a 'yyyy-MM-dd'
    return this.updProduct.packagingDate ? this.updProduct.packagingDate.toISOString().split('T')[0] : '';
  }
  
  set packagingDate(value: string) {
    // Convertimos el string 'yyyy-MM-dd' de vuelta a un objeto Date
    this.updProduct.packagingDate = new Date(value);
  }

  obSubmit(){
    this.updateProduct();
  }

  updateProduct(){
    this.productService.update(this.updProduct,this.updProduct.id).subscribe({
      next: (data) => {
        console.log('Producto actualizado.');
        this.showMessage = true;
        this.message = 'Product successfully updated.';
      },
      error: (error) => {
        console.error('Error al actualizar producto.');
        this.showMessage = true;
        this.message = 'Error updating product.';
      }
    })
  }

  ngOnInit(): void {

    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      packagingDate: ['', [Validators.required]],
      stock: ['', [Validators.required]],
      price: ['', [Validators.required]]
    });
    this.getAllProductos();
  }

  getAllProductos(){
    this.productService.getAll().subscribe({
      next: (data: Product[]) => {
        console.log('Datos de productos cargados correctamente');
        this.products = data;
      },
      error: (error) => {
        console.error('Error al cargar datos de productos.');
        this.showMessage = true;
        this.message = 'Error when loading product data.';
      }
    })
  }

  // Método para mostrar el mensaje después de un alta/modificación
  showSuccessMessage(action: string): void {
    this.message = `¡${action} exitoso!`;
    this.showMessage = true;
    
    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  showErrorMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;
    
    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.showMessage = false;
  }

}

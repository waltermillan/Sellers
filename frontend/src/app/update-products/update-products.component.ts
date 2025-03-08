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

  productForm!: FormGroup;

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

 onProductChange(): void {
  if (this.selectedProductId != null) {
    const selectedSeller = this.products.find(s => s.id == this.selectedProductId);

    if (selectedSeller) {
      this.updProduct.id = selectedSeller.id;
      this.updProduct.name = selectedSeller.name;
      this.updProduct.price = selectedSeller.price;
      this.updProduct.stock = selectedSeller.stock;
      
      if (typeof selectedSeller.packagingDate === 'string') {
        this.updProduct.packagingDate = new Date(selectedSeller.packagingDate);
      } else {
        this.updProduct.packagingDate = selectedSeller.packagingDate;
      }

      const formattedDate = this.updProduct.packagingDate?.toISOString().split('T')[0];
      this.productForm.controls['packagingDate'].setValue(formattedDate);
      } 
      else {
        console.warn('Seller not found');
      }
    }
  }
  
  get packagingDate(): string {
    return this.updProduct.packagingDate ? this.updProduct.packagingDate.toISOString().split('T')[0] : '';
  }
  
  set packagingDate(value: string) {
    this.updProduct.packagingDate = new Date(value);
  }

  obSubmit(){
    this.updateProduct();
  }

  updateProduct(){
    this.productService.update(this.updProduct,this.updProduct.id).subscribe({
      next: (data) => {
        console.log('Product successfully updated.');
        this.showMessage = true;
        this.message = 'Product successfully updated.';
      },
      error: (error) => {
        console.error('Error updating product');
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
        console.log('Products loaded successfully.');
        this.products = data;
      },
      error: (error) => {
        console.error('Error when loading product data.');
        this.showMessage = true;
        this.message = 'Error when loading product data.';
      }
    })
  }

  showSuccessMessage(action: string): void {
    this.message = `¡${action} exitoso!`;
    this.showMessage = true;
    
    setTimeout(() => this.closeMessage(), 3000);
  }

  showErrorMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;
    
    setTimeout(() => this.closeMessage(), 3000);
  }

  closeMessage(): void {
    this.showMessage = false;
  }

}

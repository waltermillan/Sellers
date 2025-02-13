import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';



@Component({
  selector: 'app-add-sellers',
  templateUrl: './add-sellers.component.html',
  styleUrl: './add-sellers.component.css'
})
export class AddSellersComponent implements OnInit {

  sellerForm!: FormGroup; // Definir el formulario reactivo

  newSeller: Seller = {   // Nuevo cliente vacío
    id: 0,
    name: '',
    birthday: null
  };

  showMessage = false;
  message = '';

  constructor(private sellerService:SellerService,
              private fb: FormBuilder) {
    
  }

  ngOnInit(){
    // Inicializar el formulario con validaciones
    this.sellerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      birthday: ['', [Validators.required]]
    });
  }

  addSeller(): void{
    this.sellerService.addSeller(this.newSeller).subscribe({
      next: (data) => {
        console.log('Vendedor agregado:', this.newSeller);
        this.showSuccessMessage('Vendedor agregado exitosamente!')
      },
      error: (error) => {
        console.error('Error al agregar el vendedor:', error);
      }
    })
  }

  onSubmit(){
    this.addSeller();
  }

    // Método para mostrar el mensaje después de un alta/modificación
  showSuccessMessage(action: string): void {
    this.message = `¡${action} exitoso!`;
    this.showMessage = true;
    
    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.showMessage = false;
  }
}

import { Component } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { BuyerService } from '../services/buyer.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-buyers',
  templateUrl: './update-buyers.component.html',
  styleUrls: ['./update-buyers.component.css']
})
export class UpdateBuyersComponent {

  buyers: Buyer[] = [];
  selectedBuyerId: number | null = null;

  showMessage = false;
  message = '';

  buyerForm!: FormGroup; // Formulario reactivo
  updBuyer: Buyer = {
    id: 0,
    name: '',
    socialSecurityNumber: 0
  };

  constructor(private buyerService: BuyerService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    // Inicializar el formulario con validaciones
    this.buyerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      socialSecurityNumber: ['', [Validators.required]]
    });

    // Cargar todos los vendedores
    this.getAllBuyers();
  }

  getAllBuyers(): void {
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;
      },
      error: (error) => {
        console.error('Error al cargar compradores', error);
        if (error.status === 0) {
          // Este es un error típico de conexión (no hay conexión al servidor)
          this.showErrorMessage('Could not connect to the server. Check your Internet connection or try again later. successful!');
        } else {
          // Otros errores de la API
          this.showErrorMessage('There was an error adding the buyer. Please try again.');
        }
      }
    });
  }

  // Maneja el cambio de selección
  onBuyerChange(): void {
    if (this.selectedBuyerId != null) {

      const selectedBuyer = this.buyers.find(s => s.id == this.selectedBuyerId);
  
      //console.log('Vendedor encontrado:', selectedSeller);
  
      if (selectedBuyer) {
        this.updBuyer.id = selectedBuyer.id;
        this.updBuyer.name = selectedBuyer.name;
        this.updBuyer.socialSecurityNumber = selectedBuyer.socialSecurityNumber;
      } 
    }
  } 

  updateBuyer(): void {

    this.buyerService.update(this.updBuyer).subscribe({
      next: (data) => {
        this.showMessage = true;
        this.message = 'Upgraded buyer';
      },
      error: (error) => {
        console.error('Error al actualizar el comprador:', error);
        this.showMessage = true;
        this.message = 'Error updating the buyer';
      }
    });
  }

  onSubmit(): void {
    this.updateBuyer();
  }

  // Método para mostrar el mensaje después de un alta/modificación
  showSuccessMessage(action: string): void {
    this.message = `¡${action} exitoso!`;
    this.showMessage = true;
    
    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  // Método para mostrar el mensaje después de un alta/modificación
  showErrorMessage(action: string): void {
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

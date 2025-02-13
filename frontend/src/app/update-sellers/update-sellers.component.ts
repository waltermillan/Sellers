import { Component } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-sellers',
  templateUrl: './update-sellers.component.html',
  styleUrls: ['./update-sellers.component.css']
})
export class UpdateSellersComponent {

  sellers: Seller[] = [];
  selectedSellerId: number | null = null;

  showMessage = false;
  message = '';

  sellerForm!: FormGroup; // Formulario reactivo
  updSeller: Seller = {
    id: 0,
    name: '',
    birthday: new Date()
  };

  constructor(private sellerService: SellerService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    // Inicializar el formulario con validaciones
    this.sellerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      birthday: ['', [Validators.required]]
    });

    // Cargar todos los vendedores
    this.getAllSellers();
  }

  getAllSellers(): void {
    this.sellerService.getAllSellers().subscribe({
      next: (data) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error('Error al cargar vendedores', error);
      }
    });
  }

  // Maneja el cambio de selección
  onSellerChange(): void {
    if (this.selectedSellerId != null) {
      console.log('Vendedor encontrado:', this.selectedSellerId);
      const selectedSeller = this.sellers.find(s => s.id == this.selectedSellerId);
  
      console.log('Vendedor encontrado:', selectedSeller);
  
      if (selectedSeller) {
        this.updSeller.id = selectedSeller.id;
        this.updSeller.name = selectedSeller.name;
        
        // Convertimos la fecha a un objeto Date si es un string
        if (typeof selectedSeller.birthday === 'string') {
          this.updSeller.birthday = new Date(selectedSeller.birthday);
        } else {
          this.updSeller.birthday = selectedSeller.birthday;
        }
  
        // Convertir la fecha al formato 'yyyy-MM-dd' para el input
        const formattedDate = this.updSeller.birthday?.toISOString().split('T')[0];
        this.sellerForm.controls['birthday'].setValue(formattedDate);  // Asignamos al formulario en formato 'yyyy-MM-dd'
      } else {
        console.warn('Vendedor no encontrado');
      }
    }
  }

  get birthday(): string {
    // Si 'birthday' es un objeto Date, lo convertimos a 'yyyy-MM-dd'
    return this.updSeller.birthday ? this.updSeller.birthday.toISOString().split('T')[0] : '';
  }
  
  set birthday(value: string) {
    // Convertimos el string 'yyyy-MM-dd' de vuelta a un objeto Date
    this.updSeller.birthday = new Date(value);
  }
  

  updateSeller(): void {
    console.log('pre-Vendedor actualizado:', this.updSeller);
    this.sellerService.updateSeller(this.updSeller).subscribe({
      next: (data) => {
        
        this.showSuccessMessage('Vendedor actualizado');

        // alert('Vendedor actualizado exitosamente!');
      },
      error: (error) => {
        console.error('Error al actualizar el vendedor:', error);
      }
    });
  }

  onSubmit(): void {
    this.updateSeller();
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

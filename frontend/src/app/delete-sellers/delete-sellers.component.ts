import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { error } from 'console';

@Component({
  selector: 'app-delete-sellers',
  templateUrl: './delete-sellers.component.html',
  styleUrl: './delete-sellers.component.css'
})
export class DeleteSellersComponent implements OnInit {

  //Declaración de variables
  public sellers: Seller[] = [];

  showMessage = false;
  message = '';

  //Constructor
  constructor(private sellerService:SellerService) {
    
  }

  //////////////////////////////////////////////Métodos//////////////////////////////////////////////

  //NgOnInit --> es un hook (gancho de ciclo de vida) en Angular que forma parte de los Lifecycle Hooks. 
  //Se ejecuta una vez que Angular ha inicializado todas las propiedades de un componente, es decir, 
  //cuando la vista y los datos del componente están listos.
  ngOnInit(){
    this.getAllSellers();
  }

  //GetAllSellers --> obtiene un array de Seller donde se guardan todos los datos de vendedores de la
  //la API de clientes.
  getAllSellers(){
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error("Error al cargar vendedores");
      }
    })
  }

  deleteSellers(id:number){
    this.sellerService.deleteSeller(id).subscribe({
      next: (data) => {
        this.showSuccessMessage('vendedor eliminado exitosamente')
        this.getAllSellers();
      },
      error: (error) => {
        console.error("Error al cargar vendedores");
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
  
    // Método para cerrar el mensaje
    closeMessage(): void {
      this.showMessage = false;
    }

    changeCursor(cursorStyle: string): void {
      document.querySelector('img')?.style.setProperty('cursor', cursorStyle);
    }

}

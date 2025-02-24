import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { MessageService } from '../services/message.service';

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
  constructor(private sellerService:SellerService,
              public messageService:MessageService
  ) {
    
  }

  //////////////////////////////////////////////Métodos//////////////////////////////////////////////

  //NgOnInit --> es un hook (gancho de ciclo de vida) en Angular que forma parte de los Lifecycle Hooks. 
  //Se ejecuta una vez que Angular ha inicializado todas las propiedades de un componente, es decir, 
  //cuando la vista y los datos del componente están listos.
  ngOnInit(){
    this.getAllSellers();
  }

  //GetAllSellers --> obtiene un array de Seller donde se guardan todos los datos de vendedores de la API de clientes.
  getAllSellers(){
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error("Error al cargar vendedores");
        if (error.status === 0) {
          // Este es un error típico de conexión (no hay conexión al servidor)
          this.messageService.showErrorMessage('Could not connect to the server. Check your Internet connection or try again later.');
        } else {
          // Otros errores de la API
          this.messageService.showErrorMessage('There was an error listing sellers. Please try again.');
        }
      }
    });
  }

  //DeleteSellers --> borra un vendedor de la lista de vendedores. los datos de vendedores de la API de clientes.
  deleteSellers(id:number){
    this.sellerService.deleteSeller(id).subscribe({
      next: (data) => {
        this.messageService.showSuccessMessage('seller successfully removed')
        this.getAllSellers();
      },
      error: (error) => {
        console.error("Error al cargar vendedores");
      }
    });
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

  changeCursor(cursorStyle: string): void {
    document.querySelector('img')?.style.setProperty('cursor', cursorStyle);
  }

}

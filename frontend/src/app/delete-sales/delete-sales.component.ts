import { Component, OnInit } from '@angular/core';
import { SaleDTO } from '../models/saleDTO.model';
import { SaleDTOService } from '../services/sale-dto.service';
import { SaleService } from '../services/sale.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-delete-sales',
  templateUrl: './delete-sales.component.html',
  styleUrl: './delete-sales.component.css'
})
export class DeleteSalesComponent implements OnInit {

  salesDTO: SaleDTO [] = [];

  constructor(private saleDTOService:SaleDTOService,
              private saleService:SaleService,
              public messageService:MessageService
  ) {

  }

  ngOnInit(): void {
    this.getAllDTOSales();
  }

  getAllDTOSales(){
    this.saleDTOService.getAll().subscribe({
      next: (data) => {
        this.salesDTO = data;
        console.log('')
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
    })
  }

  deleteSale(id:number){
    this.saleService.delete(id).subscribe({
      next: (data) => {
        console.log('Sala delete sucessfully.');
        this.getAllDTOSales();
        this.messageService.showMessage = true;
        this.messageService.message = 'Sale delete sucessfully.';
      },
      error: (error) => {
        console.error("Error al cargar ventas");
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

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }
}

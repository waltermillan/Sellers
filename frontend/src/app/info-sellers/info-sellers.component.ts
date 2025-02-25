import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';
import { GLOBAL_CONFIG } from '../config/config.global';

@Component({
  selector: 'app-info-sellers',
  templateUrl: './info-sellers.component.html',
  styleUrl: './info-sellers.component.css',
})
export class InfoSellersComponent implements OnInit {
  //Declaración de variables

  public sellers: Seller[] = [];

  //Constructor
  constructor(
    private sellerService: SellerService,
    public messageService: MessageService,
    private exportService: ExportService,
    private commonService: CommonService
  ) {}

  //////////////////////////////////////////////Métodos//////////////////////////////////////////////

  //NgOnInit --> es un hook (gancho de ciclo de vida) en Angular que forma parte de los Lifecycle Hooks.
  //Se ejecuta una vez que Angular ha inicializado todas las propiedades de un componente, es decir,
  //cuando la vista y los datos del componente están listos.
  ngOnInit() {
    this.getAllSellers();
  }

  //GetAllSellers --> obtiene un array de vendedores(Seller[]) donde se guardan todos los datos 
  // de vendedores de la API de clientes.
  getAllSellers() {
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error('Error al cargar vendedores');
        if (error.status === 0) {
          // Este es un error típico de conexión (no hay conexión al servidor)
          this.messageService.showErrorMessage(
            'Could not connect to the server. Check your Internet connection or try again later.'
          );
        } else {
          // Otros errores de la API
          this.messageService.showErrorMessage(
            'There was an error listing sellers. Please try again.'
          );
        }
      },
    });
  }

  exportToExcel(){
    const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.xls';
    this.exportService.exportTableToExcel('tblSellers', fileName);
  }

  exportToPDF(): void {
    const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.pdf';
    this.exportService.exportTableToPDF('tblSellers', fileName);
  }

  // Llama a closeMessage() del servicio
  closeMessage(): void {
    this.messageService.closeMessage();
  }
}

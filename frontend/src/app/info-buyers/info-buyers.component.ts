import { Component, OnInit } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { BuyerService } from '../services/buyer.service';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-info-buyers',
  templateUrl: './info-buyers.component.html',
  styleUrl: './info-buyers.component.css'
})
export class InfoBuyersComponent implements OnInit {

  infoFileName: string = 'Data_Buyers_'
  buyers:Buyer[] = [];

  constructor(private buyerService: BuyerService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService
  ) {
 
  }

  ngOnInit(){
    this.getAllBuyers();
  }

  //Obtiene todos los compradores
  getAllBuyers():void{
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;
      },
      error: (error) => {
        console.error('Error al cargar los datos de los compradores.');
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing buyers. Please try again.';
      }
    });
  }
 
  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

  exportToExcel(){
    const fileName = this.infoFileName + this.commonService.getFormattedDate() + '.xls';
    this.exportService.exportTableToExcel('tblBuyers', fileName)
  }

  exportToPDF(){
  const fileName = this.infoFileName + this.commonService.getFormattedDate() + '.pdf';
  this.exportService.exportTableToPDF('tblBuyers', fileName)
  }
}

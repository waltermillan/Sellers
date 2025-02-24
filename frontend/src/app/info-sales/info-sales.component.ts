import { Component, OnInit } from '@angular/core';
import { SaleDTO } from '../models/saleDTO.model';
import { SaleDTOService } from '../services/sale-dto.service';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-info-sales',
  templateUrl: './info-sales.component.html',
  styleUrl: './info-sales.component.css'
})
export class InfoSalesComponent implements OnInit {

  infoFileName: string = 'Data_Sales_';
  salesDTO: SaleDTO[] = [];

  constructor(private saleDTOService: SaleDTOService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService
  ) {

  }

  ngOnInit(): void {
    this.getAllSalesDTO();
  }

  getAllSalesDTO(){
    this.saleDTOService.getAll().subscribe({
      next: (data) => {
        console.log(data);
        this.salesDTO = data;
      },
      error: (error) => {
        console.error('Error al cargar los datos de las ventas. ', error);
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing sales. Please try again.';
      }
    });
  }

  // Método para cerrar el mensaje
   closeMessage(): void {
     this.messageService.showMessage = false;
   }

   exportToExcel(){
      const fileName = this.infoFileName + this.commonService.getFormattedDate() + '.xls';
      this.exportService.exportTableToExcel('tblSales', fileName)
   }

   exportToPDF(){
    const fileName = this.infoFileName + this.commonService.getFormattedDate() + '.pdf';
    this.exportService.exportTableToPDF('tblSales', fileName)
   }

}

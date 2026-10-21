import { Component, OnInit } from '@angular/core';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';
import { GLOBAL_CONFIG } from '../config/config.global';
import { SaleHistoricService } from '../services/saleHistoric.service';
import { SaleHistoric } from '../models/saleHistoric.model';

@Component({
  selector: 'app-info-sales-historic',
  templateUrl: './info-sales-historic.component.html',
  styleUrl: './info-sales-historic.component.css'
})
export class InfoSalesHistoricComponent implements OnInit {

  salesHistoric: SaleHistoric[] = [];

  constructor(private saleHistoricService: SaleHistoricService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService
  ) {

  }

  ngOnInit(): void {
    this.getAllSalesHistoric();
  }

  getAllSalesHistoric(){
    this.saleHistoricService.getAll().subscribe({
      next: (data) => {
      this.salesHistoric = data.sort(
        (a, b) => new Date(b.processedDate).getTime() - new Date(a.processedDate).getTime()
      );
        console.log(this.salesHistoric);
      },
      error: (error) => {
        console.error('Error al cargar los datos de las ventas. ', error);
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing sales. Please try again.';
      }
    });
  }

   closeMessage(): void {
     this.messageService.showMessage = false;
   }

   exportToExcel(){
      const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.xls';
      this.exportService.exportTableToExcel('tblSales', fileName)
   }

   exportToPDF(){
    const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.pdf';
    this.exportService.exportTableToPDF('tblSales', fileName)
   }
}

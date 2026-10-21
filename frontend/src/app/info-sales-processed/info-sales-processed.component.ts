import { Component, OnInit } from '@angular/core';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';
import { GLOBAL_CONFIG } from '../config/config.global';
import { SaleProcessedService } from '../services/saleProcessed.service';
import { SaleProcessed } from '../models/saleProcessed.model';

@Component({
  selector: 'app-info-sales-processed',
  templateUrl: './info-sales-processed.component.html',
  styleUrl: './info-sales-processed.component.css'
})
export class InfoSalesProcessedComponent implements OnInit {

  salesProcessed: SaleProcessed[] = [];

  constructor(private salesProcessedService: SaleProcessedService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService
  ) {

  }

  ngOnInit(): void {
    this.getAllSalesProcessed();
  }

  getAllSalesProcessed(){
    this.salesProcessedService.getAll().subscribe({
      next: (data) => {
      this.salesProcessed = data.sort(
        (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
      );
        console.log(this.salesProcessed);
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

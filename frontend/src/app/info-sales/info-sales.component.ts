import { Component, OnInit } from '@angular/core';
import { SaleDTO } from '../models/saleDTO.model';
import { SaleDTOService } from '../services/sale-dto.service';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';
import { GLOBAL_CONFIG } from '../config/config.global';
import { SaleService } from '../services/sale.service';
import { SaleProcessedService } from '../services/saleProcessed.service';
import { SaleHistoricService } from '../services/saleHistoric.service';
import { SaleHistoric } from '../models/saleHistoric.model';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-info-sales',
  templateUrl: './info-sales.component.html',
  styleUrl: './info-sales.component.css'
})
export class InfoSalesComponent implements OnInit {

  salesDTO: SaleDTO[] = [];

  constructor(private saleDTOService: SaleDTOService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService,
              private saleService: SaleService,
              private saleProcessedService: SaleProcessedService,
              private saleHistoricService: SaleHistoricService,
              
  ) {

  }

  ngOnInit(): void {
    this.getAllSalesDTO();
  }

  getAllSalesDTO(){
    this.saleDTOService.getAll().subscribe({
      next: (data) => {
      this.salesDTO = data.sort(
        (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
      );
        console.log(this.salesDTO);
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
   
  processSales(): void {
    this.saleProcessedService.addBulk().pipe(
      switchMap(() => {
        const saleHistoric: SaleHistoric = {
          id: 0,
          numberOfSales: this.salesDTO.length,
          status: 'SUCCESS',
          processedDate: new Date().toISOString()
        };

        return this.saleHistoricService.add(saleHistoric);
      }),
      switchMap(() => this.saleService.deleteBulk())
    ).subscribe({
      next: () => {
        this.messageService.showMessage = true;
        this.messageService.message =
          'Sales processed, historic registered and original records deleted successfully.';

        this.getAllSalesDTO();
      },
      error: (error) => {
        console.error('Error processing sales.', error);

        this.messageService.showMessage = true;
        this.messageService.message =
          error.error?.message ??
          'There was an error processing sales. Please check the data and try again.';
      }
    });
  }
}

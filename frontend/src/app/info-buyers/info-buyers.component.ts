import { Component, OnInit } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { BuyerService } from '../services/buyer.service';
import { MessageService } from '../services/message.service';
import { ExportService } from '../services/export.service';
import { CommonService } from '../services/common.service';
import { GLOBAL_CONFIG } from '../config/config.global';

@Component({
  selector: 'app-info-buyers',
  templateUrl: './info-buyers.component.html',
  styleUrl: './info-buyers.component.css'
})
export class InfoBuyersComponent implements OnInit {

  buyers:Buyer[] = [];

  constructor(private buyerService: BuyerService,
              public messageService: MessageService,
              private exportService: ExportService,
              private commonService: CommonService) 
  {}

  ngOnInit(){
    this.getAllBuyers();
  }

  getAllBuyers():void{
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;
      },
      error: (error) => {
        console.error('There was an error listing buyers.');
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing buyers. Please try again.';
      }
    });
  }
 
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

  exportToExcel(){
    const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.xls';
    this.exportService.exportTableToExcel('tblBuyers', fileName)
  }

  exportToPDF(){
  const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.pdf';
  this.exportService.exportTableToPDF('tblBuyers', fileName)
  }
}

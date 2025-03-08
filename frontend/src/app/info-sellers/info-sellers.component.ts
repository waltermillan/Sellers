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

  public sellers: Seller[] = [];

  constructor(
    private sellerService: SellerService,
    public messageService: MessageService,
    private exportService: ExportService,
    private commonService: CommonService
  ) {}

  //////////////////////////////////////////////Methods//////////////////////////////////////////////

  //NgOnInit --> is a hook (lifecycle hook) in Angular that is part of the Lifecycle Hooks.
  //It is executed once Angular has initialized all the properties of a component, ie,
  //when the component's view and data are ready.
  ngOnInit() {
    this.getAllSellers();
  }

  getAllSellers() {
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error('Error al cargar vendedores');
        if (error.status === 0) {
          this.messageService.showErrorMessage(
            'Could not connect to the server. Check your Internet connection or try again later.'
          );
        } else {
          this.messageService.showErrorMessage(
            'There was an error listing sellers. Please try again.'
          );
        }
      }
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

  closeMessage(): void {
    this.messageService.closeMessage();
  }
}

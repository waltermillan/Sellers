import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { MessageService } from '../services/message.service';
import { CommonService} from '../services/common.service';
import { ExportService} from '../services/export.service';
import { GLOBAL_CONFIG } from '../config/config.global';

@Component({
  selector: 'app-info-products',
  templateUrl: './info-products.component.html',
  styleUrl: './info-products.component.css'
})
export class InfoProductsComponent implements OnInit {

  products:Product[] = [];

  constructor(private productService: ProductService,
              public messageService: MessageService,
              private commonService: CommonService,
              private exportService: ExportService) 
  {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.productService.getAll().subscribe({
      next: (data:Product[]) => {
        this.products = data.filter(p=> p.stock > 0);
        console.log('List Products');
      },
      error: (error) => {
        console.log('There was an error listing products. Please try again.');
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing products. Please try again.'
      }
    });
  }

  exportToExcel(){
    const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.xls';
    this.exportService.exportTableToExcel('tblProducts', fileName)
  }

  exportToPDF(){
  const fileName = GLOBAL_CONFIG.infoFileName + this.commonService.getCompleteFormattedDate() + '.pdf';
  this.exportService.exportTableToPDF('tblProducts', fileName)
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

  
}

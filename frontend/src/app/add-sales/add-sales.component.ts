import { Component, OnInit  } from '@angular/core';

import { Seller } from '../models/seller.model';
import { Product } from '../models/product.model';
import { Buyer } from '../models/buyer.model';
import { Sale } from '../models/sale.model';

import { SellerService } from '../services/seller.service';
import { ProductService } from '../services/product.service';
import { BuyerService } from '../services/buyer.service';
import { SaleService } from '../services/sale.service';
import { MessageService } from '../services/message.service';
import { CommonService } from '../services/common.service';


@Component({
  selector: 'app-add-sales',
  templateUrl: './add-sales.component.html',
  styleUrl: './add-sales.component.css'
})
export class AddSalesComponent implements OnInit {

  sellers:Seller[] = [];
  products:Product[] = [];
  buyers:Buyer[] = [];

  selectedIdSeller: number | null = null;
  selectedIdProduct: number | null = null;
  selectedIdBuyer: number | null = null;

  newSale:Sale = {
    id: 0,
    idBuyer: 0,
    idProduct: 0,
    idSeller: 0,
    date: ''
  }

  isFormValid: boolean = false;

  constructor(private sellerService:SellerService,
              private productService:ProductService,
              private buyerService:BuyerService,
              private saleService:SaleService,
              public messageService:MessageService,
              private commonService: CommonService)
  {}

  ngOnInit(): void {
    this.getAllBuyers();
    this.getAllProducts();
    this.getAllSellers();
    
  }

  validateForm() {
    this.isFormValid = this.selectedIdSeller !== null && this.selectedIdProduct !== null && this.selectedIdBuyer !== null;
  }

  onSubmit(){
    this.addNewSale();
  }

  addNewSale(){
    this.newSale.idSeller = this.selectedIdSeller;
    this.newSale.idProduct = this.selectedIdProduct;
    this.newSale.idBuyer = this.selectedIdBuyer;
    this.newSale.date = this.commonService.getShortFormattedDate(new Date());

    const sale: Sale = {
      id: 0,
      idBuyer: this.selectedIdBuyer,
      idProduct: this.selectedIdProduct,
      idSeller: this.selectedIdSeller,
      date: this.commonService.getShortFormattedDate(new Date())
    };

    if(confirm('Are you sure?')){
      this.saleService.add(sale).subscribe({
        next: (data) => {
            this.messageService.showMessage = true;
            this.messageService.message = 'Sale completed successfully';
            console.log('Sale completed successfully');
        },
        error: (error) => {
          console.error('Sale failed', error);
        }
      });
    }
  }

  getAllSellers(){
    this.sellerService.getAllSellers().subscribe({
      next: (data) => {
        this.sellers = data;
      },
      error: (error) => {
        console.log('Error loading sellers.');
      }
    });
  }

  getAllBuyers(){
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;
      },
      error: (error) => {
        console.log('Error loading buyers.');
      }
    });
  }

  getAllProducts(){
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (error) => {
        console.log('Error loading products.');
      }
    });
  }

  selectItem(item: number, type:number) {

    switch(type){
      case 1:
        if (this.selectedIdSeller === item) {
          this.selectedIdSeller = null;
        } else {
          this.selectedIdSeller = item;
        }
        break;
      case 2:
        if (this.selectedIdProduct === item) {
          this.selectedIdProduct = null;
        } else {
          this.selectedIdProduct = item;
        }
        break;
      case 3:
        if (this.selectedIdBuyer === item) {
          this.selectedIdBuyer = null;
        } else {
          this.selectedIdBuyer = item;
        }
        break;
    };

    this.validateForm();
  }

  closeMessage(): void {
    this.messageService.showMessage = false;
  }
}

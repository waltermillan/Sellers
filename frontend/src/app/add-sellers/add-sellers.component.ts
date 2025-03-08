import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-add-sellers',
  templateUrl: './add-sellers.component.html',
  styleUrl: './add-sellers.component.css'
})
export class AddSellersComponent implements OnInit {

  sellerForm!: FormGroup;

  newSeller: Seller = { 
    id: 0,
    name: '',
    birthday: null
  };

  constructor(private sellerService:SellerService,
              public messageService:MessageService)
  {}

  ngOnInit(){

  }

  onSubmit(){
    this.addSeller();
  }

  addSeller(): void{
    this.sellerService.addSeller(this.newSeller).subscribe({
      next: (data) => {
        this.messageService.showSuccessMessage('Seller successfully added!')
      },
      error: (error) => {
        console.error('Error adding seller:', error);
        if (error.status === 0) {
          this.messageService.showErrorMessage('Could not connect to the server. Please check your Internet connection or try again later!');
        } else {
          this.messageService.showErrorMessage('There was an error adding the seller. Please try again.');
        }
      }
    })
  }

  closeMessage(): void {
    this.messageService.showMessage = false;
  }
}

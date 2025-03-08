import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-delete-sellers',
  templateUrl: './delete-sellers.component.html',
  styleUrl: './delete-sellers.component.css'
})
export class DeleteSellersComponent implements OnInit {

  public sellers: Seller[] = [];

  showMessage = false;
  message = '';

  constructor(private sellerService:SellerService,
              public messageService:MessageService
  ) {
    
  }

  //////////////////////////////////////////////Methods//////////////////////////////////////////////

  //NgOnInit --> is a hook (lifecycle hook) in Angular that is part of the Lifecycle Hooks.
  //It is executed once Angular has initialized all the properties of a component, ie,
  //when the component's view and data are ready.
  ngOnInit(){
    this.getAllSellers();
  }

  getAllSellers(){
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error("Error loading sellers");
        if (error.status === 0) {
          this.messageService.showErrorMessage('Could not connect to the server. Check your Internet connection or try again later.');
        } else {
          this.messageService.showErrorMessage('There was an error listing sellers. Please try again.');
        }
      }
    });
  }

  deleteSellers(id:number){
    this.sellerService.deleteSeller(id).subscribe({
      next: (data) => {
        this.messageService.showSuccessMessage('seller successfully removed')
        this.getAllSellers();
      },
      error: (error) => {
        console.error("Error loandin sellers.");
      }
    });
  }

  closeMessage(): void {
    this.messageService.showMessage = false;
  }

  changeCursor(cursorStyle: string): void {
    document.querySelector('img')?.style.setProperty('cursor', cursorStyle);
  }

}

import { Component } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { BuyerService } from '../services/buyer.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-delete-buyers',
  templateUrl: './delete-buyers.component.html',
  styleUrl: './delete-buyers.component.css'
})
export class DeleteBuyersComponent {

  buyers:Buyer[] = [];

  constructor(private buyerService:BuyerService,
              public messageService:MessageService)
{}

  ngOnInit(){
    this.getAllBuyers();
  }

  onSubmit(){
    
  }

  getAllBuyers(){
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;

      },
      error: (error) => {
        console.log('Error loading buyers.');
        this.messageService.showMessage = true;
        this.messageService.message = 'There was an error listing buyers. Please try again.'       
      }
    })
  }

  deleteBuyer(id:number)
  {
    this.buyerService.delete(id).subscribe({
      next: (data) => {
        console.log('buyer deleted successfully.');
        this.messageService.showMessage = true;
        this.messageService.message = 'Buyer successfully removed.';
        this.getAllBuyers();
      },
      error: (error) => {
        console.log('Error deleting buyer.');
        this.messageService.showMessage = true;
        this.messageService.message = 'Error deleting buyer.';
      }
    });
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }

}

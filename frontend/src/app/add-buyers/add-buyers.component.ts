import { Component, OnInit } from '@angular/core';
import { Buyer} from '../models/buyer.model';
import { BuyerService} from '../services/buyer.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-add-buyers',
  templateUrl: './add-buyers.component.html',
  styleUrl: './add-buyers.component.css'
})
export class AddBuyersComponent implements OnInit {
  
  newBuyer:Buyer = {
    id: 0,
    name: '',
    socialSecurityNumber: 0
  };

  constructor(private buyerService: BuyerService,
              public messageService:MessageService
  ) {
    
  }

  ngOnInit(){

  }
  
  onSubmit(){
    this.addBuyer();
  }

  addBuyer(){
    this.buyerService.add(this.newBuyer).subscribe({
      next: (data) => {
        console.log('Alta de un comprador nuevo');
        this.messageService.showMessage = true;
        this.messageService.message = 'Successful registration buyer';
      },
      error: (error) => {
        console.error('error al dar de alta un comprador nuevo. ', error);
        this.messageService.showMessage = true;
        this.messageService.message = 'error when registering a new buyer.';
      }
    })
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.messageService.showMessage = false;
  }
}

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
              public messageService:MessageService)
{}
  ngOnInit(){

  }
  
  onSubmit(){
    this.addBuyer();
  }

  addBuyer(){
    this.buyerService.add(this.newBuyer).subscribe({
      next: (data) => {
        console.log('Buyer addedd Successfully');
        this.messageService.showMessage = true;
        this.messageService.message = 'Buyer addedd Successfully';
      },
      error: (error) => {
        console.error('error adding buyer. ', error);
        this.messageService.showMessage = true;
        this.messageService.message = 'error adding buyer.';
      }
    })
  }

  closeMessage(): void {
    this.messageService.showMessage = false;
  }
}

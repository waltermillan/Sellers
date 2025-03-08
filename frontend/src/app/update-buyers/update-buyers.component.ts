import { Component } from '@angular/core';
import { Buyer } from '../models/buyer.model';
import { BuyerService } from '../services/buyer.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-buyers',
  templateUrl: './update-buyers.component.html',
  styleUrls: ['./update-buyers.component.css']
})
export class UpdateBuyersComponent {

  buyers: Buyer[] = [];
  selectedBuyerId: number | null = null;

  showMessage = false;
  message = '';

  buyerForm!: FormGroup;
  updBuyer: Buyer = {
    id: 0,
    name: '',
    socialSecurityNumber: 0
  };

  constructor(private buyerService: BuyerService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.buyerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      socialSecurityNumber: ['', [Validators.required]]
    });

    this.getAllBuyers();
  }

  getAllBuyers(): void {
    this.buyerService.getAll().subscribe({
      next: (data) => {
        this.buyers = data;
      },
      error: (error) => {
        console.error('Error al cargar compradores', error);
        if (error.status === 0) {
          this.showErrorMessage('Could not connect to the server. Check your Internet connection or try again later. successful!');
        } else {
          this.showErrorMessage('There was an error adding the buyer. Please try again.');
        }
      }
    });
  }

  onBuyerChange(): void {
    if (this.selectedBuyerId != null) {

      const selectedBuyer = this.buyers.find(s => s.id == this.selectedBuyerId);
    
      if (selectedBuyer) {
        this.updBuyer.id = selectedBuyer.id;
        this.updBuyer.name = selectedBuyer.name;
        this.updBuyer.socialSecurityNumber = selectedBuyer.socialSecurityNumber;
      } 
    }
  } 

  updateBuyer(): void {

    this.buyerService.update(this.updBuyer, this.updBuyer.id).subscribe({
      next: (data) => {
        this.showMessage = true;
        this.message = 'Buyer updated';
      },
      error: (error) => {
        console.error('Error updating buyer.', error);
        this.showMessage = true;
        this.message = 'Error updating buyer.';
      }
    });
  }

  onSubmit(): void {
    this.updateBuyer();
  }

  showSuccessMessage(action: string): void {
    this.message = `¡${action} successfull!`;
    this.showMessage = true;
    
    setTimeout(() => this.closeMessage(), 3000);
  }

  showErrorMessage(action: string): void {
    this.message = `¡${action} exitoso!`;
    this.showMessage = true;
    
    setTimeout(() => this.closeMessage(), 3000);
  }

  closeMessage(): void {
    this.showMessage = false;
  }
}

import { Component } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-sellers',
  templateUrl: './update-sellers.component.html',
  styleUrls: ['./update-sellers.component.css']
})
export class UpdateSellersComponent {

  sellers: Seller[] = [];
  selectedSellerId: number | null = null;

  showMessage = false;
  message = '';

  sellerForm!: FormGroup;
  updSeller: Seller = {
    id: 0,
    name: '',
    birthday: null
  };

  constructor(private sellerService: SellerService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.sellerForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      birthday: ['', [Validators.required]]
    });

    this.getAllSellers();
  }

  getAllSellers(): void {
    this.sellerService.getAllSellers().subscribe({
      next: (data) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error('Error al cargar vendedores', error);
        if (error.status === 0) {
          this.showErrorMessage('Could not connect to the server. Check your Internet connection or try again later. successful!');
        } else {
          this.showErrorMessage('There was an error adding the seller. Please try again.');
        }
      }
    });
  }

  onSellerChange(): void {
    if (this.selectedSellerId != null) {
      const selectedSeller = this.sellers.find(s => s.id == this.selectedSellerId);
  
      if (selectedSeller) {
        this.updSeller.id = selectedSeller.id;
        this.updSeller.name = selectedSeller.name;
        

        if (typeof selectedSeller.birthday === 'string') {
          this.updSeller.birthday = new Date(selectedSeller.birthday);
        } else {
          this.updSeller.birthday = selectedSeller.birthday;
        }
  
        const formattedDate = this.updSeller.birthday?.toISOString().split('T')[0];
        this.sellerForm.controls['birthday'].setValue(formattedDate);
      } else {
        console.warn('Seller not found.');
      }
    }
  }

  get birthday(): string {
    return this.updSeller.birthday ? this.updSeller.birthday.toISOString().split('T')[0] : '';
  }
  
  set birthday(value: string) {
    this.updSeller.birthday = new Date(value);
  }
  

  updateSeller(): void {
    this.sellerService.updateSeller(this.updSeller, this.updSeller.id).subscribe({
      next: (data) => {
        
        this.showSuccessMessage('Seller successfully upgraded.');
      },
      error: (error) => {
        console.error('Error updating seller.', error);
        this.showErrorMessage('Error updating seller.');
      }
    });
  }

  onSubmit(): void {
    this.updateSeller();
  }

  showSuccessMessage(action: string): void {
    this.message = `${action}`;
    this.showMessage = true;

    setTimeout(() => this.closeMessage(), 3000);
  }

  showErrorMessage(action: string): void {
    this.message = `${action}`;
    this.showMessage = true;
    
    setTimeout(() => this.closeMessage(), 3000);
  }

  closeMessage(): void {
    this.showMessage = false;
  }
}

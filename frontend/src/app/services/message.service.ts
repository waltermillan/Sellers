import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  message: string = '';
  showMessage: boolean = false;

  constructor() { }

  showSuccessMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;

    setTimeout(() => this.closeMessage(), 3000);
  }

  showErrorMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;

    setTimeout(() => this.closeMessage(), 3000);
  }

  closeMessage(): void {
    this.showMessage = false;
  }
}

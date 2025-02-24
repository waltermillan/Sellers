import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  message: string = '';
  showMessage: boolean = false;

  constructor() { }

  // Método para mostrar el mensaje de éxito
  showSuccessMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;

    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  // Método para mostrar el mensaje de error
  showErrorMessage(action: string): void {
    this.message = `¡${action}!`;
    this.showMessage = true;

    // Cerrar el mensaje después de 3 segundos
    setTimeout(() => this.closeMessage(), 3000);
  }

  // Método para cerrar el mensaje
  closeMessage(): void {
    this.showMessage = false;
  }
}

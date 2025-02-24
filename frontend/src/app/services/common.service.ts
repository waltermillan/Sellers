import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

  getFormattedDate(){
    const now = new Date();

    const year = now.getFullYear();
    const month = (now.getMonth() + 1).toString().padStart(2, '0'); // Asegura que el mes siempre tenga 2 dígitos
    const day = now.getDate().toString().padStart(2, '0'); // Asegura que el día siempre tenga 2 dígitos
    const hours = now.getHours().toString().padStart(2, '0'); // Asegura que las horas siempre tengan 2 dígitos
    const minutes = now.getMinutes().toString().padStart(2, '0'); // Asegura que los minutos siempre tengan 2 dígitos
    const seconds = now.getSeconds().toString().padStart(2, '0'); // Asegura que los segundos siempre tengan 2 dígitos

    // Combina los valores en el formato deseado
    return `${year}${month}${day}${hours}${minutes}${seconds}`;
   }
}

import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import { error } from 'console';

@Component({
  selector: 'app-info-sellers',
  templateUrl: './info-sellers.component.html',
  styleUrl: './info-sellers.component.css'
})
export class InfoSellersComponent implements OnInit {

  //Declaración de variables
  public sellers: Seller[] = [];

  //Constructor
  constructor(private sellerService:SellerService) {
    
  }

  //////////////////////////////////////////////Métodos//////////////////////////////////////////////

  //NgOnInit --> es un hook (gancho de ciclo de vida) en Angular que forma parte de los Lifecycle Hooks. 
  //Se ejecuta una vez que Angular ha inicializado todas las propiedades de un componente, es decir, 
  //cuando la vista y los datos del componente están listos.
  ngOnInit(){
    this.getAllSellers();
  }

  //GetAllSellers --> obtiene un array de Seller donde se guardan todos los datos de vendedores de la
  //la API de clientes.
  getAllSellers(){
    this.sellerService.getAllSellers().subscribe({
      next: (data: Seller[]) => {
        this.sellers = data;
      },
      error: (error) => {
        console.error("Error al cargar vendedores");
      }
    })
  }
  


}

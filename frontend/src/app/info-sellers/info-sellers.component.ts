import { Component, OnInit } from '@angular/core';
import { Seller } from '../models/seller.model';
import { SellerService } from '../services/seller.service';
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';


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
  
  exportToExcel(): void {
    // Crear un arreglo de objetos con los datos de los vendedores
    const sellersForExcel = this.sellers.map(seller => ({
      ID: seller.id,
      Name: seller.name,
      Birthday: seller.birthday ? new Date(seller.birthday).toLocaleDateString('es-ES') : ''
    }));
  
    // Crear un libro de trabajo (workbook)
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(sellersForExcel);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
  
    // Añadir la hoja de trabajo al libro
    XLSX.utils.book_append_sheet(wb, ws, 'Vendedores');
  
    // Generar y descargar el archivo Excel
    XLSX.writeFile(wb, 'vendedores.xlsx');
  }
  

  exportToPDF(): void {
  const doc = new jsPDF();

  // Definir un título para el PDF
  doc.setFontSize(16);
  doc.text('Vendedores', 20, 20);

  // Establecer la fuente para los datos
  doc.setFontSize(12);

  // Crear las cabeceras de la tabla
  const headers = ['ID', 'Name', 'Birthday'];
  let y = 30; // Y es la posición vertical inicial

  // Imprimir las cabeceras
  headers.forEach((header, index) => {
    doc.text(header, 20 + index * 60, y);  // Espaciado horizontal entre las columnas
  });

  // Imprimir los datos de los vendedores
  this.sellers.forEach((seller, index) => {
    y += 10; // Mover hacia abajo para la siguiente fila
    doc.text(String(seller.id), 20, y);
    doc.text(seller.name, 80, y);
    // Convertir la fecha a string (asegurarse de que esté en formato 'dd/MM/yyyy')
    const birthday = seller.birthday ? new Date(seller.birthday).toLocaleDateString('es-ES') : '';
    doc.text(birthday, 140, y);  // Verificar si la fecha existe
  });

    // Guardar el archivo PDF
    doc.save('vendedores.pdf');
  }
}

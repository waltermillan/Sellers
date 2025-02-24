import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable'; // Importación correcta de autoTable

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  constructor() {}

  exportTableToExcel(tableId: string, filename: string = 'archivo.xlsx'): void {
    // Obtener la tabla HTML
    const table: HTMLTableElement = document.getElementById(tableId) as HTMLTableElement;
  
    // Convertir la tabla a un arreglo de datos
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(table);  // Eliminamos la opción { header: 1 }
  
    // Crear un libro de trabajo (workbook)
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Hoja1');
  
    // Exportar el archivo Excel
    XLSX.writeFile(wb, filename);
  }
  

  // Función genérica para exportar tabla HTML a PDF
  exportTableToPDF(tableId: string, tableName:string): void {
    // Obtiene la tabla HTML por su ID
    const table = document.getElementById(tableId);

    if (!table) {
      console.error('La tabla no se encontró');
      return;
    }

    // Crear una instancia de jsPDF
    const doc = new jsPDF();

    // Usamos autoTable para manejar mejor las tablas HTML en PDF
    autoTable(doc, {
      html: `#${tableId}`, // Esto pasa el ID de la tabla HTML
      startY: 20, // Puedes ajustar esto según sea necesario
    });


    doc.save(tableName);
  }
}

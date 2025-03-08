import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  constructor() {}

  exportTableToExcel(tableId: string, filename: string = 'archivo.xlsx'): void {
    
    // Get HTML table
    const table: HTMLTableElement = document.getElementById(tableId) as HTMLTableElement;
  
    // Convert table to data array
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(table);  // Delete option { header: 1 }
  
    // Create a work book (workbook)
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Hoja1');
  
    // Export excel file
    XLSX.writeFile(wb, filename);
  }
  
  exportTableToPDF(tableId: string, tableName:string): void {
    // Get HTML table HTML by ID
    const table = document.getElementById(tableId);

    if (!table) {
      console.error('Table not found');
      return;
    }

    // Create jsPDF instance
    const doc = new jsPDF();

    // We use autoTable to handle HTML tables in PDF
    autoTable(doc, {
      html: `#${tableId}`,
      startY: 20,
    });

    doc.save(tableName);
  }
}

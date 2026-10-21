export interface SaleProcessed {
    id: number;
    idBuyer:number | null;
    idProduct: number | null;
    idSeller:number | null;
    amount: number | null;
    quantity: number | null;
    date:string;
}

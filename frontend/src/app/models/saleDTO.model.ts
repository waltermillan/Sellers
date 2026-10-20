export interface SaleDTO {
    id: number;

    idSeller:number;
    sellerName:string;

    idBuyer:number;
    buyerName:string;

    idProduct:number;
    productName:string;

    amount:number;
    quantity:number;

    date: Date;
}

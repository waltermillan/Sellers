import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InfoSellersComponent } from './info-sellers/info-sellers.component';
import { AddSellersComponent } from './add-sellers/add-sellers.component';
import { UpdateSellersComponent } from './update-sellers/update-sellers.component';
import { DeleteSellersComponent } from './delete-sellers/delete-sellers.component';

import { InfoBuyersComponent } from './info-buyers/info-buyers.component';
import { AddBuyersComponent } from './add-buyers/add-buyers.component';
import { UpdateBuyersComponent } from './update-buyers/update-buyers.component';
import { DeleteBuyersComponent } from './delete-buyers/delete-buyers.component';

import { InfoProductsComponent } from './info-products/info-products.component';
import { AddProductsComponent } from './add-products/add-products.component';
import { UpdateProductsComponent } from './update-products/update-products.component';
import { DeleteProductsComponent } from './delete-products/delete-products.component';

import { InfoSalesComponent } from './info-sales/info-sales.component';
import { AddSalesComponent } from './add-sales/add-sales.component';
import { UpdateSalesComponent } from './update-sales/update-sales.component';
import { DeleteSalesComponent } from './delete-sales/delete-sales.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },  // Redirige a login al inicio
  { path: 'info-sellers', component: InfoSellersComponent },
  { path: 'add-sellers', component: AddSellersComponent },
  { path: 'update-sellers', component: UpdateSellersComponent },
  { path: 'delete-sellers', component: DeleteSellersComponent },
  { path: 'info-buyers', component: InfoBuyersComponent },
  { path: 'add-buyers', component: AddBuyersComponent },
  { path: 'update-buyers', component: UpdateBuyersComponent },
  { path: 'delete-buyers', component: DeleteBuyersComponent },
  { path: 'info-products', component: InfoProductsComponent },
  { path: 'add-products', component: AddProductsComponent },
  { path: 'update-products', component: UpdateProductsComponent },
  { path: 'delete-products', component: DeleteProductsComponent },
  { path: 'info-sales', component: InfoSalesComponent },
  { path: 'add-sales', component: AddSalesComponent },
  { path: 'update-sales', component: UpdateSalesComponent },
  { path: 'delete-sales', component: DeleteSalesComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

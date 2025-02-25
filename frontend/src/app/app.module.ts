import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';  // Importa FormsModule

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { InfoSellersComponent } from './info-sellers/info-sellers.component';
import { AddSellersComponent } from './add-sellers/add-sellers.component';
import { UpdateSellersComponent } from './update-sellers/update-sellers.component';
import { DeleteSellersComponent } from './delete-sellers/delete-sellers.component';
import { InfoProductsComponent } from './info-products/info-products.component';
import { AddProductsComponent } from './add-products/add-products.component';
import { UpdateProductsComponent } from './update-products/update-products.component';
import { DeleteProductsComponent } from './delete-products/delete-products.component';
import { InfoBuyersComponent } from './info-buyers/info-buyers.component';
import { AddBuyersComponent } from './add-buyers/add-buyers.component';
import { UpdateBuyersComponent } from './update-buyers/update-buyers.component';
import { DeleteBuyersComponent } from './delete-buyers/delete-buyers.component';
import { InfoSalesComponent } from './info-sales/info-sales.component';
import { UpdateSalesComponent } from './update-sales/update-sales.component';
import { AddSalesComponent } from './add-sales/add-sales.component';
import { DeleteSalesComponent } from './delete-sales/delete-sales.component';
import { LoginComponent } from './login/login.component';
import { PrincipalComponent } from './principal/principal.component';
import { InfoUsersComponent } from './info-users/info-users.component';


@NgModule({
  declarations: [
    AppComponent,
    InfoSellersComponent,
    AddSellersComponent,
    UpdateSellersComponent,
    DeleteSellersComponent,
    InfoProductsComponent,
    AddProductsComponent,
    UpdateProductsComponent,
    DeleteProductsComponent,
    InfoBuyersComponent,
    AddBuyersComponent,
    UpdateBuyersComponent,
    DeleteBuyersComponent,
    InfoSalesComponent,
    UpdateSalesComponent,
    AddSalesComponent,
    DeleteSalesComponent,
    LoginComponent,
    PrincipalComponent,
    InfoUsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi(), withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

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


@NgModule({
  declarations: [
    AppComponent,
    InfoSellersComponent,
    AddSellersComponent,
    UpdateSellersComponent,
    DeleteSellersComponent
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

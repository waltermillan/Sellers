import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InfoSellersComponent } from './info-sellers/info-sellers.component';
import { AddSellersComponent } from './add-sellers/add-sellers.component';
import { UpdateSellersComponent } from './update-sellers/update-sellers.component';
import { DeleteSellersComponent } from './delete-sellers/delete-sellers.component';

const routes: Routes = [
  { path: 'info-sellers', component: InfoSellersComponent },
  { path: 'add-sellers', component: AddSellersComponent },  
  { path: 'update-sellers', component: UpdateSellersComponent },
  { path: 'delete-sellers', component: DeleteSellersComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

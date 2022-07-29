import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ListTransactionsComponent } from './list-transactions/list-transactions.component';
import { PFMApiService } from './pfm-api.service';
import { RouterModule, Routes } from '@angular/router';
import { CategorizeComponent } from './categorize/categorize.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';

const appRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'transactions',
  },
  {
    path: 'transactions',
    component: ListTransactionsComponent,
  },
];

@NgModule({
  declarations: [AppComponent, ListTransactionsComponent, CategorizeComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes, {
      enableTracing: false, // <-- debugging purposes only
    }),
    BrowserAnimationsModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatSelectModule,
  ],
  providers: [PFMApiService],
  bootstrap: [AppComponent],
})
export class AppModule {}

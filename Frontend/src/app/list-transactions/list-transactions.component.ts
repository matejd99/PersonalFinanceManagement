import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Transaction } from 'src/model/transaction';
import { CategorizeComponent } from '../categorize/categorize.component';
import { PFMApiService } from '../pfm-api.service';

@Component({
  selector: 'app-list-transactions',
  templateUrl: './list-transactions.component.html',
  styleUrls: ['./list-transactions.component.css'],
})
export class ListTransactionsComponent implements OnInit {
  TransacionList$: Transaction[] = [];
  displayedColumns = [
    'Id',
    'BeneficiaryName',
    'Date',
    'Direction',
    'Ammount',
    'Description',
    'Currency',
    'MCC',
    'Kind',
    'Category',
    'Categorize',
  ];

  constructor(private service: PFMApiService, public dialog: MatDialog) {}

  private GetTransactionsList() {
    this.service
      .GetTransactionsList()
      .subscribe((t) => (this.TransacionList$ = t));
  }

  categorize(transaction: Transaction) {
    const dialogRef = this.dialog.open(CategorizeComponent, {
      width: '400px',
      data: { transactionId: transaction.id },
    });

    dialogRef.afterClosed().subscribe((request) => {
      if (request) {
        this.service.Categorieze(transaction.id, request).subscribe((t) => {
          const i = this.TransacionList$.findIndex(
            (tt) => tt.id == transaction.id
          );
          if (i >= 0) {
            this.TransacionList$[i] = t;
          }
        });
      }
    });
  }

  ngOnInit(): void {
    this.GetTransactionsList();
  }
}

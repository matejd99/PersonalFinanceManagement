import { Component, OnInit } from '@angular/core';
import { Observable, observable } from 'rxjs';
import { PFMApiService } from '../pfm-api.service';

@Component({
  selector: 'app-list-transactions',
  templateUrl: './list-transactions.component.html',
  styleUrls: ['./list-transactions.component.css']
})
export class ListTransactionsComponent implements OnInit {

  TransacionList$!:Observable<any[]>;

  constructor(private service:PFMApiService) { }

  ngOnInit(): void {
    this.TransacionList$ = this.service.GetTransactionsList();
  }

}

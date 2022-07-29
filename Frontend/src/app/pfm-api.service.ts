import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from 'src/model/category';
import { TransactionCategorizeCommand } from 'src/model/models';
import { Transaction } from 'src/model/transaction';

@Injectable({
  providedIn: 'root',
})
export class PFMApiService {
  readonly PFMApiUrl = 'http://localhost:5255';

  constructor(private http: HttpClient) { }

  GetTransactionsList(
    transactionKind?: string,
    startDate?: string,
    endDate?: string,
    page?: number,
    pageSize?: number
  ): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.PFMApiUrl + '/transactions', {
      params: {
        transactionKind: transactionKind ?? 'pmt',
        // startDate: startDate,
        // endDate: endDate,
        // page: page,
        // pageSize: pageSize,
      } as any,
    });
  }

  GetCategoriesList(): Observable<Category[]> {
    return this.http.get<Category[]>(this.PFMApiUrl + '/categories');
  }

  Categorieze(
    id: number,
    request: TransactionCategorizeCommand
  ): Observable<Transaction> {
    return this.http.post<Transaction>(
      this.PFMApiUrl + '/transactions/' + id + '/categorize',
      request
    );
  }
}

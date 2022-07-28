import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PFMApiService {

  readonly PFMApiUrl = "http://localhost:5255/"

  constructor(private http:HttpClient) { }

  GetTransactionsList():Observable<any[]>{
    return this.http.get<any>(this.PFMApiUrl + "/transactions/Transactions");
  }


}

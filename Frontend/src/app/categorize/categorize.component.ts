import { Component, OnInit } from '@angular/core';
import {
  MatDialogRef,
} from '@angular/material/dialog';
import { Category } from 'src/model/category';
import { PFMApiService } from '../pfm-api.service';

@Component({
  selector: 'app-categorize',
  templateUrl: './categorize.component.html',
  styleUrls: ['./categorize.component.css'],
})
export class CategorizeComponent implements OnInit {
  CategoriesList$: Category[] = [];
  selection: string = '';

  constructor(
    public dialogRef: MatDialogRef<CategorizeComponent>,
    private service: PFMApiService
  ) {}

  onOk() {
    this.dialogRef.close({ CategoryCode: this.selection });
  }

  ngOnInit(): void {
    this.service
      .GetCategoriesList()
      .subscribe((c) => (this.CategoriesList$ = c));
  }
}

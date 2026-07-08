import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';

interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
}

interface Product {
  id: number;
  name: string;
}

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  baseUrl = 'http://localhost:5000/api/';
  private http = inject(HttpClient);
  title = signal('client');
  products = signal<Product[]>([]);


    ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseUrl + 'products').subscribe({
      next: response => {
        this.products.set(response.data);
      },
      error: error => {
        console.log(error);
      },
      complete: () => {
        console.log('Request completed');
      }
    });
  }


}

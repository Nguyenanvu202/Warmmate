import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';
import { ShopService } from './core/service/shop.service';
import { ShopComponent } from "./feature/shop/shop.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'WarmMate';
  searchValue: string = '';
  onSearch(searchName: string): void {
    console.log('Search value:', searchName);
    this.searchValue = searchName;
    console.log('Search name:', this.searchValue);
  }

}

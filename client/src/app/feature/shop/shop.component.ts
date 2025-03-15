import { Component, inject, Input, signal, SimpleChanges } from '@angular/core';
import { ShopService } from '../../core/service/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { FilteringPanelComponent } from "./filtering-panel/filtering-panel.component";
import { ShopParams } from '../../shared/models/shopParams';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, 
    FilteringPanelComponent,
    MatPaginatorModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent {
  private shopService = inject(ShopService);
  products? : Pagination<Product> ;
  shopParams = new ShopParams();
@Input() searchValue: string = '';
  ngOnInit(): void {
    this.initializeShop();
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('Search value changed:', changes['searchValue']);
    if (changes['searchValue']) {
      this.onSearchChange();
    }
  }

  initializeShop(){
    this.shopService.getColor();
    this.shopService.getSize();
    this.getProducts();
  }

  getProducts(): void {
    this.shopService.getProduct(this.shopParams).subscribe({
      next:response=> { 
        console.log(response.value);
        
        this.products = response.value;},
      error: error => console.log(error),
      complete: () =>console.log('complete')
      
    })
  }
  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    console.log('Updated shopParams:', this.shopParams);
    this.getProducts();
  }
  onOptionsSelectionChange(shopParams: ShopParams): void {
    console.log('Selected Options', shopParams);
    this.shopParams = shopParams;
    this.shopParams.pageNumber = 1;
    this.shopParams.search = this.searchValue;
    console.log('Updated shopParams:', this.shopParams);
    this.getProducts();
  }

onSearchChange(): void {
  console.log('Search value:', this.searchValue);
    this.shopParams.pageNumber = 1;
    this.shopParams.search = this.searchValue;
    console.log('Updated shopParams:', this.shopParams);
    this.getProducts();
  }
  
}

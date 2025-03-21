import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/service/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { FilteringPanelComponent } from "./filtering-panel/filtering-panel.component";
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { SharedDataService } from '../../core/service/shared-data.service';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, FilteringPanelComponent, MatPaginatorModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private sharedData = inject(SharedDataService);
  products?: Pagination<Product>;
  shopParams = new ShopParams();

  ngOnInit(): void {
    this.initializeShop();
    this.onSearchChanges();
  }

  initializeShop(): void {
    this.shopService.getColorOption();
    this.shopService.getSizeOption();
    this.getProducts();
  }

  getProducts(): void {
    this.shopService.getProduct(this.shopParams).subscribe({
      next: response => {
        console.log(response.value);
        this.products = response.value;
      },
      error: error => console.log(error),
      complete: () => console.log('complete')
    });
  }

  handlePageEvent(event: PageEvent): void {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onOptionsSelectionChange(shopParams: ShopParams): void {
    this.shopParams = shopParams;
    this.shopParams.pageNumber = 1;
    this.shopParams.search = this.sharedData.getSharedString();
    this.getProducts();
  }

  onSearchChanges(): void {
    this.sharedData.search$.subscribe(value => {
      this.shopParams.search = value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    });
  }
}
import { Component, inject } from '@angular/core';
import { ShopService } from '../../core/service/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent {
  private shopService = inject(ShopService);
  products : Product[] = [];
  ngOnInit(): void {
    this.shopService.getProduct().subscribe({
      next:response=> { 
        console.log(response.value),
        this.products = response.value.data},
      error: error => console.log(error),
      complete: () =>console.log('complete')
      
    })
  }


}

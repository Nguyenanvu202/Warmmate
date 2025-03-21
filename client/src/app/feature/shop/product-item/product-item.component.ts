import { Component, inject, Input, signal, SimpleChanges } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import {MatButtonToggleChange, MatButtonToggleModule} from '@angular/material/button-toggle';
import { ShopService } from '../../../core/service/shop.service';
import { Option } from '../../../shared/models/option';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../../core/services/cart.service';
@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [NgOptimizedImage,
    RouterLink, 
    MatButtonToggleModule,
    FormsModule
  ],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() item?: Product;
  private shopService = inject(ShopService);
  selectedSize: string = ''
  selectedColor:string = ''
  cartService = inject(CartService);
  productColor?: Option[];
  productSize?: Option[];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['item'] && changes['item'].currentValue) {
      this.loadProduct(); // Reload product when `item` changes
    }
  }
  loadProduct() {
    const id = this.item?.id;
    console.log("productID",id);
    if (!id) return;
    this.shopService.getColorByProduct(+id).subscribe({
      next: (response) => {
        
        this.productColor = response.value;
      },
      error: (error) => console.log(error),
      complete: () => console.log('complete'),
      
    });

    this.shopService.getSizeByProduct(+id).subscribe({
      next: (response) => {
        this.selectedSize = response.value[0];

        this.productSize = response.value;
        if (this.productSize && this.productSize.length > 0) {
          this.selectedSize = this.productSize[0].value; 
        }
      },
      error: (error) => console.log(error),
      complete: () => console.log('complete'),
      
    });
  }

  onSizeSelected(event: MatButtonToggleChange){
    this.selectedSize = event.value;
    console.log('selected size:', this.selectedSize)
  }

  onColorSelected(event: MatButtonToggleChange){
    this.selectedColor = event.value;
    console.log('selected size:', this.selectedColor)
  }


  hideSingleSelectionIndicator = signal(false);
  hideMultipleSelectionIndicator = signal(false);

  toggleSingleSelectionIndicator() {
    this.hideSingleSelectionIndicator.update(value => !value);
  }

  toggleMultipleSelectionIndicator() {
    this.hideMultipleSelectionIndicator.update(value => !value);
  }
}

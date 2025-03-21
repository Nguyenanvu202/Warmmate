import { Component, inject, NgModule, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/service/shop.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {MatButtonToggleChange, MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatButtonModule} from '@angular/material/button';
import { Option } from '../../shared/models/option';
import {MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import {MatInput, MatInputModule} from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../core/services/cart.service';
import { FormsModule } from '@angular/forms';

declare var $: any;
@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [MatButtonToggleModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule
  ],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'], // Corrected 'styleUrl' to 'styleUrls'
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  private cartService = inject(CartService);
  productItem?: Product;
  productColor?: Option[];
  productSize?: Option[];
  selectedColor: string = '';
  selectedSize: string = '';
  quantity = 1;
  quantityInCart = 0;
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(id);
    if (!id) return;
    this.shopService.getProductById(+id).subscribe({
      next: (response) => {
        console.log(response.value);
        this.productItem = response;
      },
      error: (error) => console.log(error),
      complete: () => console.log('complete'),
    });

    this.shopService.getColorByProduct(+id).subscribe({
      next: (response) => {
        console.log("Color",response.value);
        this.productColor = response.value;
      },
      error: (error) => console.log(error),
      complete: () => console.log('complete'),
      
    });

    this.shopService.getSizeByProduct(+id).subscribe({
      next: (response) => {
        console.log(response);
        this.productSize = response.value;
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
  updateCart() {
    if (!this.productItem) return;
    if (this.quantity > this.quantityInCart) {
      const itemsToAdd = this.quantity - this.quantityInCart;
      this.quantityInCart += itemsToAdd;
      this.cartService.addItemtoCart(this.productItem, this.selectedColor, this.selectedSize, itemsToAdd);
    } else {
      const itemsToRemove = this.quantityInCart - this.quantity;
      this.quantityInCart -= itemsToRemove;
      this.cartService.removeItemFromCart(this.productItem.id, itemsToRemove);
    }
  }

  updateQuantityInCart() {
    this.quantityInCart = this.cartService.cart()?.items
      .find(x => x.itemId === this.productItem?.id)?.quantity || 0;
    this.quantity = this.quantityInCart || 1;
  }

  getButtonText() {
    return this.quantityInCart > 0 ? 'Update cart' : 'Add to cart'
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

import { Component, inject, Input } from '@angular/core';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';
import { Address } from '../../../shared/models/user';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-checkout-review',
  standalone: true,
  imports: [
    CurrencyPipe,
    CommonModule

],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss'
})
export class CheckoutReviewComponent {
  cartService = inject(CartService);
  @Input() addressUser!: Address;
  @Input() fullName?: string;

}
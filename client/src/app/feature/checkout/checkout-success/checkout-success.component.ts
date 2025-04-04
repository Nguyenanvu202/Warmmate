import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { SignalrService } from '../../../core/services/signalr.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { OrderService } from '../../../core/services/order.service';
import { Order } from '../../../shared/models/order';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    MatProgressSpinnerModule,
    DatePipe,
    CurrencyPipe,
    NgIf
  ],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss'
})
export class CheckoutSuccessComponent implements OnInit {
  private orderService = inject(OrderService);
  lastOrder?: Order
  ngOnInit(): void {
    this.getLastOrder();
  }
  getLastOrder(){
    this.orderService.getOrdersForUser().subscribe(orders => {
      if (orders.length > 0) {
        this.lastOrder = orders[orders.length - 1]; // Get last item
        console.log('Last order:', this.lastOrder);
        // Do something with lastOrder
      }
    });

    
  }

}
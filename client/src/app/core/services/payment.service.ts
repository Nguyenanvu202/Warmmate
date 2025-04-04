import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../../shared/models/cart';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  returnUrl?: string;
  payUrl?: string;
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);

  paymentWithCart(cartId?: string) {
    return this.http.post<any>(this.baseUrl + "payment/" + cartId, null); // Adjust endpoint as needed
  }
  checkStatus(){
    return this.http.get<any>(this.baseUrl + "payment")
  }
}
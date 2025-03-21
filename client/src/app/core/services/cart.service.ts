import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import {  map, take, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  private lastItemId = +(localStorage.getItem('lastItemId') || 0);
  
  itemCount = computed(()=>{
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity,0)
  });
totals = computed(() => {
  const cart = this.cart();
  if(!cart) return null;
  const subtotal = cart.items.reduce((sum,item) => sum + item.price * item.quantity, 0);
  const shipping = 0;
  const discount = 0;
  return{
    subtotal,
    shipping,
    discount,
    total: subtotal + shipping - discount
  }
})
  getCart(id: string) {
    return this.http.get<any>(this.baseUrl + '/cart?id=' + id).pipe(
      map(cart =>{
        this.cart.set(cart);
        return cart;
      })
    )
  }
  setCart(cart: Cart) {
    return this.http.post<any>(this.baseUrl + '/cart', cart).subscribe({
      next: (cart) => {
        console.log("setCart"); // Log to the console
        this.cart.set(cart);    // Update the cart state
      },
    });
  }

  removeItemFromCart(itemId: number, quantity = 1){
    const cart = this.cart();
    if(!cart) return;
    const index = cart.items.findIndex(x => x.itemId === itemId);
    if(index !== -1){
      if(cart.items[index].quantity > quantity){
        cart.items[index].quantity -=quantity;
      } else cart.items.splice(index, 1);
      if(cart.items.length ===0){
        this.deleteCart();
      }else this.setCart(cart);
    }
  }
  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
      next: () =>{
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      }
    })
  }

  addItemtoCart(item: CartItem | Product, color: string, size: string, quantity = 1){
    
    const cart = this.cart() ?? this.createCart()
    if(this.isProduct(item)){
      
      item = this.mapProductToCartItem(item, color, size, cart.items);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    this.setCart(cart);
  }
  private addOrUpdateItem(items: CartItem[], item: CartItem, quantity: number): CartItem[] {
    const index = items.findIndex(x => x.itemId === item.itemId);
    if(index===-1){
      item.quantity = quantity;
      items.push(item);
    }
    else{
      items[index].quantity += quantity 
    }
    return items
  }
private mapProductToCartItem(product: Product, color: string, size: string, items: CartItem[]): CartItem{
  const existItem = items.find((x) =>  x.productId === product.id && x.color === color && x.size === size);
  if(existItem){
    return existItem;
  }
  this.lastItemId += 1; // Increment the last used itemId
  localStorage.setItem('lastItemId', this.lastItemId.toString());
  return{
    itemId: this.lastItemId,
    productName: product.name,
    price: product.price,
    quantity: 0,
    pictureUrl: product.productItemImgs[0].imageUrl,
    color: color,
    productId: product.id,
    size: size

  }
}
   
 private isProduct(item: CartItem | Product): item is Product{
  return (item as Product) !== undefined;
 }

  private createCart():Cart{
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}

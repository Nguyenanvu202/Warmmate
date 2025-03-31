import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import {  map, take, tap } from 'rxjs';
import { DeliveryMethod } from '../../shared/models/deliveryMethod';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  private lastItemId = +(localStorage.getItem('lastItemId') || 0);
  selectedDelivery = signal<DeliveryMethod | null>(null);
  itemCount = computed(()=>{
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity,0)
  });
totals = computed(() => {
  const cart = this.cart();
  const delivery = this.selectedDelivery();
  if(!cart) return null;
  const subtotal = cart.items.reduce((sum,item) => sum + item.price * item.quantity, 0);
  const shipping = delivery?delivery.price: 0;
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
        console.log("setCart"); 
        this.cart.set(cart);    
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
    this.http.delete(this.baseUrl+ '/' + 'cart?id=' + this.cart()?.id).subscribe({
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
  private mapProductToCartItem(product: any, color: string, size: string, items: CartItem[]): CartItem {
    console.log("Checking for existing item with Product ID:", product.productId, "Color:", color, "Size:", size);
    
    const existProduct = items.find((x) => x.productId === product.id || x.productId === product.productId  && x.color === color && x.size === size);
    console.log("existItem found: ", existProduct);
  
    if (existProduct != null) {
      console.log("Item exists:", existProduct);
      return existProduct;
    }
    
    console.log("Item does not exist, creating new item");
    
    this.lastItemId += 1; // Increment the last used itemId
    localStorage.setItem('lastItemId', this.lastItemId.toString());
    
    return {
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

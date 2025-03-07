import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl ='https://localhost:5001/api/'
  private http = inject(HttpClient);
  getProduct(){
    return this.http.get<any>(this.baseUrl + 'items?pageSize=5')
  }

 
}

import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Option } from '../../shared/models/option';
import { ShopParams } from '../../shared/models/shopParams';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl ='https://localhost:5001/api/'
  private http = inject(HttpClient);
  colors: Option[] = [];
  sizes: Option[] = [];
  getProduct(shopParams: ShopParams){
    let params = new HttpParams();

    if(shopParams.options.length > 0){
      params = params.append('options', shopParams.options.join(','));
    }

    if(shopParams.search){
      params = params.append('search', shopParams.search);
    }

    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);
    return this.http.get<any>(this.baseUrl + 'items', {params})
  }
  getColor(){
    if(this.colors.length > 0) return;
    return this.http.get<any>(this.baseUrl + 'items/color').subscribe({
      next: response => this.colors = response.value

    })
  }

  getSize(){
    if(this.sizes.length > 0) return;
    return this.http.get<any>(this.baseUrl + 'items/size').subscribe({

      next: response => {
        console.log(response.value),
        this.sizes = response.value}
    }
    )
  }
 
}

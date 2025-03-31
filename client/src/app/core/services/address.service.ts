import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from '../../shared/models/user';
import { AccountService } from './account.service';
@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private baseUrl = 'https://localhost:4200/api/api-tinhthanh';
  private addressService = inject(AccountService);
  
  constructor(private http: HttpClient) { }
  getProvinces() {
    return this.http.get(`${this.baseUrl}/1/0.htm`);
  }

  getDistricts(provinceId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/2/${provinceId}.htm`);
  }

  getWards(districtId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/3/${districtId}.htm`);
  }


}

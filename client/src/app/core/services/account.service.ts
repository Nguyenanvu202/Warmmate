import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Address, User } from '../../shared/models/user';
import { map, tap } from 'rxjs/operators';
import { SignalrService } from './signalr.service';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl ='https://localhost:5001/api/'
  private http = inject(HttpClient);
  private signalrService = inject(SignalrService);
  currentUser = signal<User|null>(null);

  login(values: any){
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<User>(this.baseUrl + 'login', values, {params}).pipe(
      tap(() => this.signalrService.createHubConnection())
    )
  }
 
  register(values: any){
    return this.http.post(this.baseUrl + 'account/register', values)
  }

  getUserInfo() {
    return this.http.get<User>(this.baseUrl + 'account/user-info').pipe(
      map(user => {
        this.currentUser.set(user);
        return user;
      })
    )
  }

  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {}).pipe(
      tap(() => this.signalrService.stopHubConnection())
    )
  }

  updateAddress(address: Address) {
    return this.http.post(this.baseUrl + 'account/address', address);
  }

  getAuthState() {
    return this.http.get<{isAuthenticated: boolean}>(this.baseUrl + 'account/auth-status');
  }
}

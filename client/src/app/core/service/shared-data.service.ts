import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {
  private sharedData: string = '';
  constructor() { }

    // Method to update the value of the BehaviorSubject
  // Method to update the shared string
  private searchSubject = new BehaviorSubject<string>('');
  search$ = this.searchSubject.asObservable();

  setSharedString(value: string): void {
    this.searchSubject.next(value);
  }

  getSharedString(): string {
    return this.searchSubject.value;
  }
}

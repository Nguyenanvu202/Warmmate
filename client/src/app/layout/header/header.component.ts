import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {MatBadge, MatBadgeModule} from '@angular/material/badge';

import {MatIconModule} from '@angular/material/icon';
import { ShopComponent } from '../../feature/shop/shop.component';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatBadge,
    MatIconModule,
    FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  searchValue: string = '';
  @Output() searchChange = new EventEmitter<string>();

  onSearch(): void {
    // Pass the search value to ShopComponent
    
      this.searchChange.emit(this.searchValue);
    
}
}

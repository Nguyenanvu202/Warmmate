import {
  Component,
  EventEmitter,
  inject,
  Inject,
  Output,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatBadge, MatBadgeModule } from '@angular/material/badge';
import { MatProgressBarModule } from '@angular/material/progress-bar';

import { MatIconModule } from '@angular/material/icon';
import { ShopComponent } from '../../feature/shop/shop.component';
import { SharedDataService } from '../../core/service/shared-data.service';
import { ActivatedRoute, ActivatedRouteSnapshot, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../core/service/busy.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatBadge,MatProgressBarModule, 
    MatIconModule, FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  searchValue: string = '';
  private router = inject(Router);
  @Output() searchChange = new EventEmitter<string>();
  private shareData = inject(SharedDataService);
  onSearch(): void {
    this.shareData.setSharedString(this.searchValue);
    this.router.navigate(['/shop']);
  }

  busyService = inject(BusyService);
}

import {
  Component,
  EventEmitter,
  inject,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatBadge, MatBadgeModule } from '@angular/material/badge';
import { MatProgressBarModule } from '@angular/material/progress-bar';

import { MatIconModule } from '@angular/material/icon';
import { ShopComponent } from '../../feature/shop/shop.component';
import { SharedDataService } from '../../core/service/shared-data.service';
import { ActivatedRoute, ActivatedRouteSnapshot, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../core/service/busy.service';
import { CartService } from '../../core/services/cart.service';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { LoginComponent } from '../../feature/account/login/login.component';
import { AccountService } from '../../core/services/account.service';
import {MatMenu, MatMenuItem, MatMenuModule, MatMenuTrigger} from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatBadge,MatProgressBarModule, 
    MatIconModule, FormsModule, RouterLink, 
    RouterLinkActive,
    MatMenuTrigger,
    MatMenu,
    MatDivider,
    MatMenuItem
    ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  searchValue: string = '';
  private router = inject(Router);
  @Output() searchChange = new EventEmitter<string>();
  cartService = inject(CartService)
  private dialogService = inject(MatDialog);
  private shareData = inject(SharedDataService);
  onSearch(): void {
    this.shareData.setSharedString(this.searchValue);
    this.router.navigate(['/shop']);
  }

  busyService = inject(BusyService);
  accountService = inject(AccountService);

  openLoginDialog(){
    const dialogRef = this.dialogService.open(LoginComponent)
  };

  logout(){
    this.accountService.logout().subscribe({
      next: () =>{

          this.accountService.currentUser.set(null);
        this.router.navigateByUrl('/');
      }
    })
  }
  
}

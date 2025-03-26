import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../../feature/account/login/login.component';
import { map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  const dialogService = inject(MatDialog);
  if (accountService.currentUser()) {
    return of(true);
  } else {
    return accountService.getAuthState().pipe(
      map((auth) => {
        if (auth.isAuthenticated) {
          return true;
        } else {
           dialogService.open(LoginComponent)
           router.navigate([router.url], { 
            queryParams: { returnUrl: state.url } 
          });
          return false;
        }
      })
    );
  }
};

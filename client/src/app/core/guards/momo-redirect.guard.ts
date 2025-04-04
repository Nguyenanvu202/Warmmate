import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const momoRedirectGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  
  if (window.location.href.includes('partnerCode=MOMO')) {
    router.navigate(['/orders'], { replaceUrl: true });
    return false;
  }
  return true;
};
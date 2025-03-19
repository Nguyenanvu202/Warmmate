import { HttpInterceptorFn } from '@angular/common/http';
import { NavigationExtras, Router } from '@angular/router';
import { inject } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackbar = inject(SnackbarService);
  return next(req).pipe(
    catchError((err) => {
      if (err) {
        switch (err.status) {
          case 400:
            if (err.error.errors) {
              const modalStateErrors = [];
              for (const key in err.error.errors) {
                if (err.error.errors[key]) {
                  modalStateErrors.push(err.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            } else {
              snackbar.error(err.error.title || err.error);
            }
            break;
          case 401:
            snackbar.error(err.error.title || err.error);
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: err.error}}
            router.navigateByUrl('/server-errors', navigationExtras);
            break;
          default:
            snackbar.error(err.error.title || err.error);
            break;
        }
      }
      return throwError(() => err);
    })
  );
};

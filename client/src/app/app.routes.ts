import { Routes } from '@angular/router';
import { HomeComponent } from './feature/home/home.component';
import { ShopComponent } from './feature/shop/shop.component';
import { ProductDetailsComponent } from './feature/product-details/product-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorsComponent } from './shared/components/server-errors/server-errors.component';
import { TestErrorsComponent } from './feature/test-errors/test-errors.component';
import { CartComponent } from './feature/cart/cart.component';
import { CheckoutComponent } from './feature/checkout/checkout.component';
import { LoginComponent } from './feature/account/login/login.component';
import { RegisterComponent } from './feature/account/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { emptyCartGuard } from './core/guards/empty-cart.guard';
import { CheckoutSuccessComponent } from './feature/checkout/checkout-success/checkout-success.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'shop', component: ShopComponent},
    {path: 'detail/:id', component: ProductDetailsComponent},
    {path: 'cart', component: CartComponent},
    {path: 'checkout', component: CheckoutComponent, canActivate: [authGuard, emptyCartGuard]},
    {path: 'checkout/success', component: CheckoutSuccessComponent, canActivate: [authGuard]},
    {path: 'account/register', component: RegisterComponent},
    {path: 'test-errors', component: TestErrorsComponent},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-errors', component: ServerErrorsComponent},
    {path: 'server-errors', component: ServerErrorsComponent},
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
    
];

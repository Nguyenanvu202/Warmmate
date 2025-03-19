import { Routes } from '@angular/router';
import { HomeComponent } from './feature/home/home.component';
import { ShopComponent } from './feature/shop/shop.component';
import { ProductDetailsComponent } from './feature/product-details/product-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorsComponent } from './shared/components/server-errors/server-errors.component';
import { TestErrorsComponent } from './feature/test-errors/test-errors.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'shop', component: ShopComponent},
    {path: 'detail/:id', component: ProductDetailsComponent},
    {path: 'test-errors', component: TestErrorsComponent},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-errors', component: ServerErrorsComponent},
    {path: 'server-errors', component: ServerErrorsComponent},
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
    
];

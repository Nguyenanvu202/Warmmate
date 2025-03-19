import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/service/shop.service';
import { SlickCarouselModule } from 'ngx-slick-carousel'; 
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'], // Corrected 'styleUrl' to 'styleUrls'
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);

  productItem?: Product;

  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(id);
    if (!id) return;
    this.shopService.getProductById(+id).subscribe({
      next: (response) => {
        console.log(response);
        this.productItem = response;
      },
      error: (error) => console.log(error),
      complete: () => console.log('complete'),
    });
  }


  

}

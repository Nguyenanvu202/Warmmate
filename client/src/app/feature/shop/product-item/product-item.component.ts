import { Component, Input } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { NgOptimizedImage } from '@angular/common';
@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [NgOptimizedImage],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() item?: Product;

}

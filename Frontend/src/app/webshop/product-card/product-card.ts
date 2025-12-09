import {Component, Input} from '@angular/core';
import {Product} from '../../product/product';
import {HttpClient} from '@angular/common/http';

@Component({
	selector: 'app-product-card',
	imports: [],
	templateUrl: './product-card.html',
	styleUrl: './product-card.css',
})
export class ProductCard {
	@Input({required: true}) product!: Product;
}

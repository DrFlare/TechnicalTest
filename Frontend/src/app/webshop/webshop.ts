import {Component, inject, OnInit} from '@angular/core';
import {Product} from '../product/product';
import {ProductApi} from '../product/product-api';
import {ProductCard} from './product-card/product-card';

@Component({
	selector: 'app-webshop',
	imports: [
		ProductCard
	],
	templateUrl: './webshop.html',
	styleUrl: './webshop.css',
})
export class Webshop implements OnInit {
	protected products: Product[] = [];
	private productService: ProductApi = inject(ProductApi);

	ngOnInit(): void {
		this.productService.getProducts().subscribe({
			next: (data) => this.products = data,
			error: (err) => console.error('Failed to retrieve products', err)
		});
	}
}

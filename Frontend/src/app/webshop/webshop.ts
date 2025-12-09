import {Component, inject, OnInit, signal, WritableSignal} from '@angular/core';
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
	private productService: ProductApi = inject(ProductApi);
	protected products: WritableSignal<Product[]> = signal([]);
	protected isLoading: WritableSignal<boolean> = signal(false);
	public columnCount: number = 5;

	ngOnInit(): void {
		this.isLoading.set(true);
		this.productService.getProducts().subscribe({
			next: (data) => this.products.set(data),
			error: (err) => {
				console.error('Failed to retrieve products', err);
				this.isLoading.set(false);
			},
			complete: () => { this.isLoading.set(false); }
		});
	}
}

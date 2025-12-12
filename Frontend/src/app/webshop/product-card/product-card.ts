import {Component, inject, Input} from '@angular/core';
import {Product} from '../../product/product';
import getSymbolFromCurrency from 'currency-symbol-map'
import {MatIcon} from '@angular/material/icon';
import {CartApi} from '../../cart/cart-api';
import {CartRequestModel} from '../../cart/models/cart-request-model';

@Component({
	selector: 'app-product-card',
	imports: [
		MatIcon
	],
	templateUrl: './product-card.html',
	styleUrl: './product-card.css',
})
export class ProductCard {
	@Input({required: true}) product!: Product;
	protected cartService = inject(CartApi);
	protected selectedQuantity: number = 1;

	protected getCurrencyLabel(currencyCode: string): string {
		return getSymbolFromCurrency(currencyCode) ?? currencyCode;
	}

	protected createRequestModel() : CartRequestModel {
		return {
			productId: this.product.id,
			quantity: this.selectedQuantity
		}
	}
}

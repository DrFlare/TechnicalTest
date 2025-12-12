import {Component, inject} from '@angular/core';
import {CartApi} from './cart-api';

@Component({
	selector: 'app-cart',
	imports: [],
	templateUrl: './cart.html',
	styleUrl: './cart.css',
})
export class Cart {
	private cartService: CartApi = inject(CartApi);

}

import {inject, Injectable, Signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {CartModel} from './models/cart-model';
import {CartRequestModel} from './models/cart-request-model';

@Injectable({
  providedIn: 'root',
})
export class CartApi {
	private http: HttpClient = inject(HttpClient);
	private cart: Signal<CartModel> | undefined;

	public addToCart(requestBody: CartRequestModel): Observable<CartModel>{
		console.log('addToCart');
		let cartId = localStorage.getItem('cartId');
		if (cartId !== null) {
			requestBody.cartId = cartId;
		}
		// unfinished, unfortunately
		return this.http.post<CartModel>(environment.backendUrl + '/cart/add', requestBody);
	}
}

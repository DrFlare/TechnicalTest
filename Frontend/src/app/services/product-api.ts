import {Injectable} from '@angular/core';
import {Product} from '../models/product';
import {Observable} from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class ProductApi {
	getProducts(): Observable<Product[]>{
		// TODO: API call prema backendu ovdje?
		return new Observable<Product[]>();	// TODO: placeholder, makni
	}
}

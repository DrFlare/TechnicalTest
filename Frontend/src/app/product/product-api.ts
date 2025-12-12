import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Product} from './product';
import {environment} from '../../environments/environment';

@Injectable({
	providedIn: 'root',
})
export class ProductApi {
	private http: HttpClient = inject(HttpClient);

	public getProducts(): Observable<Product[]>{
		return this.http.get<Product[]>(environment.backendUrl + '/products');
	}

	public addDummyProducts(): void {
		this.http.get<Product[]>(environment.backendUrl + '/loadDummyData');
	}
}

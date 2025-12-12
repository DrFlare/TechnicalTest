import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
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
		this.http.get<HttpResponse<null>>(environment.backendUrl + '/loadDummyData').subscribe({
			next: (response: HttpResponse<any>) => {
				console.log(response);
				if (response.status === 200) {
					window.location.reload();
				}
			},
			error: (error) => {
				console.error('Failed to load dummy data:', error);
			}
		});
	}
}

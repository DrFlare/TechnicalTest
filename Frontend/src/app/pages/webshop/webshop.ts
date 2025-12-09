import {Component} from '@angular/core';
import {Product} from '../../models/product';

@Component({
	selector: 'app-webshop',
	imports: [],
	templateUrl: './webshop.html',
	styleUrl: './webshop.css',
})
export class Webshop {
	private products: Product[];
}

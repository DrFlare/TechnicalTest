import {Routes} from '@angular/router';
import {Webshop} from './webshop/webshop';
import {Cart} from './cart/cart';

export const routes: Routes = [
	{path: '', component: Webshop},
	{path: 'cart', component: Cart}
];

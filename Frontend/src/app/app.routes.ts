import {Routes} from '@angular/router';
import {Webshop} from './features/webshop/webshop';
import {Cart} from './features/cart/cart';

export const routes: Routes = [
	{path: '', component: Webshop},
	{path: 'cart', component: Cart}
];

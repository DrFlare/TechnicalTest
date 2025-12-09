import {Routes} from '@angular/router';
import {Webshop} from './pages/webshop/webshop';
import {Cart} from './pages/cart/cart';

export const routes: Routes = [
	{path: '', component: Webshop},
	{path: 'cart', component: Cart}
];

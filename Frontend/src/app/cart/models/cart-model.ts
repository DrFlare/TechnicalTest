import {CartItemModel} from './cart-item-model';

export interface CartModel {
	id: string;	// GUID
	items: CartItemModel[];
	validThrough: number;
}

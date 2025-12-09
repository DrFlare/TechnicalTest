export interface Product {
	id: string;	// GUID
	name: string;
	price: number;
	currencyCode: string;
	quantity: number;
	description?: string;
}

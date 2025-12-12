export interface CartRequestModel {
	cartId?: string; // GUID
	productId: string; // GUID
	quantity: number;
}

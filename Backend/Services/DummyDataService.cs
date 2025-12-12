using Backend.Models;

namespace Backend.Services;

public class DummyDataService
{
	private readonly ProductModel[] _dummyProducts = 
	[
		new(id: Guid.NewGuid(), name: "Bread", price: 3, currencyCode: "EUR", description: "500g wheat bread, freshly baked today", quantity: 20),
		new(id: Guid.NewGuid(), name: "Milk", price: 2, currencyCode: "EUR", description: "1L whole cow milk in a carton", quantity: 120),
		new(id: Guid.NewGuid(), name: "Chocolate", price: 2, currencyCode: "EUR", description: "100g milk chocolate", quantity: 30),
	];

	public ProductModel[] GetDummyProducts() => _dummyProducts;
}
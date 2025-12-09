using Backend.Models;

namespace Backend.Services;

public class DummyDataService
{
	private readonly ProductModel[] _dummyProducts = 
	[
		new(id: Guid.NewGuid(), name: "Bread", price: 3, currencyCode: "EUR", description: "Ya eat it.", quantity: 20),
		new(id: Guid.NewGuid(), name: "Milk", price: 2, currencyCode: "EUR", description: "Ya dink it.", quantity: 120),
		new(id: Guid.NewGuid(), name: "Chocolate", price: 2, currencyCode: "EUR", description: "Ya nom it.", quantity: 30),
	];

	public ProductModel[] GetDummyProducts() => _dummyProducts;
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models;

public class ProductModel
{
	public ProductModel()
	{
	}

	[SetsRequiredMembers]
	public ProductModel(Guid id, string name, decimal price, string currencyCode, string description, int quantity)
	{
		Id = id;
		Name = name;
		Price = price;
		CurrencyCode = currencyCode;
		Description = description;
		Quantity = quantity;
	}

	public Guid Id { get; set; }
	
	[StringLength(30)]
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	
	[StringLength(4)]
	public string? CurrencyCode { get; set; }
	
	[StringLength(100)]
	public string? Description { get; set; }
}
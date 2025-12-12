using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("products")]
public class ProductController(ProductService productService, InventoryService inventoryService) : ControllerBase
{
	[HttpGet]
	public IEnumerable<ProductModel> Get()
	{
		return inventoryService.GetProducts();
	}
	 
	[HttpPost]
	public async Task<ProductModel> Add([FromBody] ProductModel product)
	{
		return await productService.AddProduct(product);
	}
}
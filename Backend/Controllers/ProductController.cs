using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("products")]
public class ProductController(ProductService productService) : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<ProductModel>> Get()
	{
		return await productService.Get();
	}
	 
	[HttpPost]
	public async Task<ProductModel> Add([FromBody] ProductModel product)
	{
		return await productService.Add(product);
	}
}
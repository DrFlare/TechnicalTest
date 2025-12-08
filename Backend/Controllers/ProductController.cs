using Backend.DataAccess;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Controllers;

[ApiController]
[Route("/products/[controller]")]
public class ProductController(ProductService productService) : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<ProductModel>> Get()
	{
		return await productService.Get();
	}
	 
	[HttpGet]
	public async Task<EntityEntry<ProductModel>> Add(ProductModel product)
	{
		return await productService.Add(product);
	}
}
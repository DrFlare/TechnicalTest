using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("cart")]
public class CartController(ProductService productService, CartService cartService) : ControllerBase
{
	[HttpPost]
	public async Task Add([FromBody] Guid itemId, [FromBody] int amount)
	{
		// TODO: provjeri je li taj item dostupan
	}
}
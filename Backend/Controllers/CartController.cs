using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("cart/[action]")]
public class CartController(ICartService cartService) : ControllerBase
{
	[HttpPost]
	public async Task<CartModel> Add([FromBody] CartRequestBody request)
	{
		if (request.CartId == Guid.Empty)
		{
			request.CartId = (await cartService.CreateNewCart()).Id;
		}
		return await cartService.AddToCart(request.CartId, new CartItem(request.ProductId, request.Quantity));
	}
	
	[HttpPost]
	public async Task<CartModel> Remove([FromBody] CartRequestBody request)
	{
		return await cartService.RemoveFromCart(request.CartId, new CartItem(request.ProductId, request.Quantity));
	}

	[HttpGet("{cartId}")]
	public async Task<CartModel?> GetCartData(string cartId)
	{
		return await cartService.GetCartById(Guid.Parse(cartId));
	}
	
	[HttpGet]
	public async Task<CartModel?> CreateCart()
	{
		return await cartService.CreateNewCart();
	}
	

	[HttpGet("{cartId}")]
	public async Task<bool> CompletePurchase(string cartId)
	{
		return await cartService.CompletePurchase(Guid.Parse(cartId));
	}
}
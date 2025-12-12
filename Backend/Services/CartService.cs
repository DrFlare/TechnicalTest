using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class CartService(WebshopDbContext dbContext, InventoryService inventoryService)
{
	public CartModel CreateNewCart()
	{
		var cart = new CartModel(Guid.NewGuid());
		dbContext.Carts.Add(cart);
		return cart;
	}

	public async Task<bool> AddToCart(Guid cartId, CartItem item)
	{
		var cart = await dbContext.Carts.FindAsync(cartId) ?? CreateNewCart();
		if (inventoryService.MakeReservation(item) == false)
		{
			// New item reservation unsuccessful
			return false;
		}
		cart.AddItem(item);
		await dbContext.SaveChangesAsync();
		return true;
	}

	public async Task RemoveFromCart(Guid cartId, CartItem item)
	{
		var cart = await GetCartById(cartId);
		if (cart == null)
		{
			return;
		}
		inventoryService.CancelReservation(item);
		cart.RemoveItem(item);
		await dbContext.SaveChangesAsync();
	}
	
	public async Task EmptyCart(CartModel cart)
	{
		inventoryService.CancelCartReservation(cart);
		cart.EmptyCart();
		await dbContext.SaveChangesAsync();
	}
	
	public async Task DeleteCart(CartModel cart)
	{
		inventoryService.CancelCartReservation(cart);
		dbContext.Carts.Remove(cart);
		await dbContext.SaveChangesAsync();
	}
	
	public async Task DeleteMultipleCarts(IEnumerable<CartModel> carts)
	{
		dbContext.Carts.RemoveRange(carts);
		await dbContext.SaveChangesAsync();
		_ = inventoryService.UpdateCacheFromDb();
	}
	
	public async Task<List<CartModel>> GetCarts()
	{
		return await dbContext.Carts.ToListAsync();
	}
	public async Task<CartModel?> GetCartById(Guid cartId)
	{
		return await dbContext.Carts.FindAsync(cartId);
	}
}
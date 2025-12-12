using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class CartService(WebshopDbContext dbContext, InventoryService inventoryService): ICartService
{
	public async Task<CartModel> CreateNewCart()
	{
		var cart = new CartModel(Guid.NewGuid());
		dbContext.Carts.Add(cart);
		await dbContext.SaveChangesAsync();
		return cart;
	}

	public async Task<CartModel> AddToCart(Guid cartId, CartItem item)
	{
		var cart = await dbContext.Carts.FindAsync(cartId) ?? await CreateNewCart();
		if (inventoryService.MakeReservation(item) == false)
		{
			// New item reservation unsuccessful
			return cart;
		}
		cart.AddItem(item);
		await dbContext.SaveChangesAsync();
		return cart;
	}

	public async Task<CartModel> RemoveFromCart(Guid cartId, CartItem item)
	{
		var cart = await GetCartById(cartId);
		if (cart == null)
		{
			return cart;
		}
		inventoryService.CancelReservation(item);
		cart.RemoveItem(item);
		await dbContext.SaveChangesAsync();
		return cart;
	}

	public async Task<bool> CompletePurchase(Guid cartId)
	{
		var cart = await GetCartById(cartId);
		if (cart == null || await inventoryService.RemoveItemsFromStock(cart) == false)
		{
			return false;
		}
		await EmptyCart(cart);
		return true;
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
		inventoryService.UpdateReservations(await GetCarts());
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
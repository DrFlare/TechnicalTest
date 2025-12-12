using Backend.Models;

namespace Backend.Services;

public interface ICartService
{
	Task<CartModel> CreateNewCart();
	Task<CartModel> AddToCart(Guid cartId, CartItem item);
	Task<CartModel> RemoveFromCart(Guid cartId, CartItem item);
	Task<bool> CompletePurchase(Guid cartId);
	Task EmptyCart(CartModel cart);
	Task DeleteCart(CartModel cart);
	Task DeleteMultipleCarts(IEnumerable<CartModel> carts);
	Task<List<CartModel>> GetCarts();
	Task<CartModel?> GetCartById(Guid cartId);
}
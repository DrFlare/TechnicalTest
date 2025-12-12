using Backend.Models;

namespace Backend.Services;

public class InventoryService(ProductService productService, CartService cartService) : IHostedService
{
	private readonly Dictionary<Guid, ProductModel> _productCache = new();
	private readonly Dictionary<Guid, int> _productReservations = new();

	public IEnumerable<ProductModel> GetProducts()
	{
		// Returns products that are not reserved in other users' carts
		var availableProducts = new Dictionary<Guid, ProductModel>(_productCache);
		foreach (var reservation in _productReservations)
		{
			availableProducts[reservation.Key].Quantity -= reservation.Value;
		}

		return availableProducts.Values;
	}

	public bool MakeReservation(CartItem cartItem)
	{
		if (!_productCache.TryGetValue(cartItem.ProductId, out var product) || product.Quantity < cartItem.Quantity)
		{
			return false;
		}

		if (!_productReservations.ContainsKey(cartItem.ProductId))
		{
			_productReservations.Add(cartItem.ProductId, cartItem.Quantity);
		}
		else
		{
			_productReservations[cartItem.ProductId] += cartItem.Quantity;
		}

		return true;
	}

	public void CancelReservation(CartItem cartItem)
	{
		if (!_productReservations.ContainsKey(cartItem.ProductId))
		{
			return;
		}
		_productReservations[cartItem.ProductId] -= cartItem.Quantity;
	}

	public void CancelCartReservation(CartModel cart)
	{
		foreach (var cartItem in cart.Items)
		{
			if (!_productReservations.ContainsKey(cartItem.ProductId)) continue;
			_productReservations[cartItem.ProductId] -= cartItem.Quantity;
		}
	}


	public async Task<bool> CompletePurchase(Guid cartId)
	{
		var cart = await cartService.GetCartById(cartId);
		if (cart == null)
		{
			return false;
		}

		var productsToUpdate = new List<ProductModel>();
		var bError = false;
		foreach (var cartItem in cart.Items)
		{
			var result = await productService.TryFindProductById(cartItem.ProductId);
			if (result.Item1 == false)
			{
				// Product not found
				bError = true;
				break;
			}

			result.Item2!.Quantity -= cartItem.Quantity;
			productsToUpdate.Add(result.Item2);
		}

		await cartService.EmptyCart(cart);

		if (bError)
		{
			return false;
		}

		await productService.UpdateProducts(productsToUpdate);
		return true;
	}

	public async Task UpdateCacheFromDb()
	{
		_productCache.Clear();
		var products = await productService.GetProducts();
		foreach (var product in products)
		{
			_productCache[product.Id] = product;
		}

		var validCarts = await cartService.GetCarts();
		_productReservations.Clear();
		foreach (var cart in validCarts)
		{
			foreach (var cartItem in cart.Items)
			{
				_productCache[cartItem.ProductId].Quantity -= cartItem.Quantity;
			}
		}
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		await UpdateCacheFromDb();
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
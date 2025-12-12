using Backend.Models;

namespace Backend.Services;

public class InventoryService
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly ICartService _cartService;
	private readonly Dictionary<Guid, ProductModel> _productCache = new();
	private readonly Dictionary<Guid, int> _productReservations = new();

	public InventoryService(IServiceScopeFactory scopeFactory)
	{
		_scopeFactory = scopeFactory;
		UpdateCacheFromDb().GetAwaiter().GetResult();
	}

	public IEnumerable<ProductModel> GetProducts()
	{
		// Returns products that are not reserved in other users' carts
		// TODO: THIS SUCKS: MAKE A DEEP COPY
		var availableProducts = _productCache.Values.ToList().ConvertAll(ProductModel.Copy);
		foreach (var reservation in _productReservations)
		{
			availableProducts.First(product => product.Id == reservation.Key).Quantity -= reservation.Value;
		}

		return availableProducts;
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
	
	public async Task<bool> RemoveItemsFromStock(CartModel cart)
	{
		using var scope = _scopeFactory.CreateScope();
		var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
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

		if (bError)
		{
			return false;
		}

		await productService.UpdateProducts(productsToUpdate);
		return true;
	}

	public async Task UpdateCacheFromDb()
	{
		using var scope = _scopeFactory.CreateScope();
		var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
		_productCache.Clear();
		var products = await productService.GetProducts();
		foreach (var product in products)
		{
			_productCache[product.Id] = product;
		}
	}

	public void UpdateReservations(IEnumerable<CartModel> carts)
	{
		_productReservations.Clear();
		foreach (var cart in carts)
		{
			foreach (var cartItem in cart.Items)
			{
				_productCache[cartItem.ProductId].Quantity -= cartItem.Quantity;
			}
		}
	}
}
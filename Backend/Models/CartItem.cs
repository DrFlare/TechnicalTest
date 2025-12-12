namespace Backend.Models;

public class CartItem(Guid productId, int quantity)
{
	public Guid ProductId { get; } = productId;
	public int Quantity { get; set; } = quantity;

	public override bool Equals(object? obj)
	{
		// Quantity is not important, identify cart items by their product ID
		return obj is CartItem cartItem && ProductId.Equals(cartItem.ProductId);
	}

	public override int GetHashCode()
	{
		return ProductId.GetHashCode();
	}
}
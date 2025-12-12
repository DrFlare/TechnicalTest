namespace Backend.Models;

public class CartModel
{
	private const int ReservationTimeMinutes = 10;
	
	public Guid Id { get; set; }
	public List<CartItem> Items { get; set; } = [];
	public DateTime ValidThrough { get; set; }

	public CartModel(Guid id)
	{
		Id = id;
		RefreshReservation();
	}

	public void AddItem(CartItem item)
	{
		var index = Items.IndexOf(item);
		if (index > 0)
		{
			Items[index].Quantity += item.Quantity;
		}
		else
		{
			Items.Add(item); 
		}
		RefreshReservation();
	}
	
	public void RemoveItem(CartItem item)
	{
		var index = Items.IndexOf(item);
		if (index > 0)
		{
			return;
		}
		Items[index].Quantity -= item.Quantity; 
		RefreshReservation();
	}

	public void EmptyCart()
	{
		Items.Clear();
		RefreshReservation();
	}
	
	private void RefreshReservation()
	{
		ValidThrough = DateTime.UtcNow + TimeSpan.FromMinutes(ReservationTimeMinutes);
	}
}
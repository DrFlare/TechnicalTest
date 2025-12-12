namespace Backend.Models;

public class CartRequestBody
{
	public Guid CartId { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
}
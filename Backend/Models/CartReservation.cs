namespace Backend.Models;

public class CartReservation
{
	public CartModel Cart { get; set; }
	public Timer ReservationTimer { get; set; }

	public CartReservation(Guid reservationId, CartModel cart, TimerCallback reservationEndCallback, float reservationTimeMinutes = 10)
	{
		Cart = cart;
		var time = TimeSpan.FromMinutes(reservationTimeMinutes);
		ReservationTimer = new Timer(reservationEndCallback, null, time, time);
	}
}
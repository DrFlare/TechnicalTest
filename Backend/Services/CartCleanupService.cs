using Backend.Models;

namespace Backend.Services;

public class CartCleanupService(IServiceScopeFactory scopeFactory) : BackgroundService
{
	private readonly TimeSpan _maxCleanupInterval = TimeSpan.FromMinutes(5);
	
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using var scope = scopeFactory.CreateScope();
			var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();
			
			var nextCleanupDelay = _maxCleanupInterval.Ticks;
			var carts = await cartService.GetCarts();
			var now = DateTime.UtcNow.Ticks;
			var expiredCarts = new List<CartModel>();
			foreach (var cart in carts)
			{
				var remainingTime = cart.ValidThrough.Ticks - now;
				if (remainingTime < 0)
				{
					expiredCarts.Add(cart);
				}
				else if (remainingTime < nextCleanupDelay)
				{
					nextCleanupDelay = remainingTime;
				}
			}
			
			await cartService.DeleteMultipleCarts(expiredCarts);

			await Task.Delay(new TimeSpan(nextCleanupDelay), stoppingToken);
		}
	}
}
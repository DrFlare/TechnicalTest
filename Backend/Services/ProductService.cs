using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Services;

public class ProductService(WebshopDbContext dbContext)
{
	public async Task<IEnumerable<ProductModel>> Get()
	{
		return await dbContext.Products.ToListAsync();
	}
	 
	public async Task<EntityEntry<ProductModel>> Add(ProductModel product)
	{
		var result = await dbContext.Products.AddAsync(product);
		await dbContext.SaveChangesAsync();
		return result;
	}
}
using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class ProductService(WebshopDbContext dbContext, DummyDataService dummyDataService)
{
	public async Task<IEnumerable<ProductModel>> Get()
	{
		return await dbContext.Products.ToListAsync();
	}
	 
	public async Task<ProductModel> Add(ProductModel product)
	{
		var result = await dbContext.Products.AddAsync(product);
		await dbContext.SaveChangesAsync();
		return result.Entity;
	}
	
	public async Task ResetAndLoadDummyData()
	{
		await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Products");
		await dbContext.Products.AddRangeAsync(dummyDataService.GetDummyProducts());
		await dbContext.SaveChangesAsync();
	}
}
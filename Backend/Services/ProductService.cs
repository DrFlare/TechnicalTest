using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class ProductService(WebshopDbContext dbContext, DummyDataService dummyDataService): IProductService
{
	public async Task<List<ProductModel>> GetProducts()
	{
		return await dbContext.Products.ToListAsync();
	}

	public async Task<Tuple<bool, ProductModel?>> TryFindProductById(Guid productId)
	{
		var product = await dbContext.Products.FindAsync(productId);
		return new Tuple<bool, ProductModel?>(product == null, product);
	}

	public async Task<ProductModel> AddProduct(ProductModel product)
	{
		var result = await dbContext.Products.AddAsync(product);
		await dbContext.SaveChangesAsync();
		return result.Entity;
	}
	
	public async Task UpdateProducts(IEnumerable<ProductModel> products)
	{
		dbContext.Products.UpdateRange(products);
		await dbContext.SaveChangesAsync();
	}

	public async Task ResetAndLoadDummyData()
	{
		await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Products");
		await dbContext.Products.AddRangeAsync(dummyDataService.GetDummyProducts());
		await dbContext.SaveChangesAsync();
	}
}
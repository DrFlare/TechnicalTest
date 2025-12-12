using Backend.Models;

namespace Backend.Services;

public interface IProductService
{
	Task<List<ProductModel>> GetProducts();
	Task<Tuple<bool, ProductModel?>> TryFindProductById(Guid productId);
	Task<ProductModel> AddProduct(ProductModel product);
	Task UpdateProducts(IEnumerable<ProductModel> products);
	Task ResetAndLoadDummyData();
}
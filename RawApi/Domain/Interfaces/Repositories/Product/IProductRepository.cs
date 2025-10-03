using RawApi.Domain.Entities;

namespace RawApi.Domain
{
    public interface IProductRepository
    {
        Task SaveProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductById(int id);
        Task<bool> UpdateProductAsync(Product product);
    }
}

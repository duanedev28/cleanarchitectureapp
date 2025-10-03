using RawApi.Domain.Entities;

namespace RawApi.Application
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<bool> UpdateProductAsync(Product product);
    }
}

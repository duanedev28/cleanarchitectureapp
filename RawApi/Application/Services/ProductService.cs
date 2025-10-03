using RawApi.Domain;
using RawApi.Domain.Entities;

namespace RawApi.Application
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product == null) 
            {
                throw new NullReferenceException("Product should not be null");
            }

            await _productRepository.SaveProductAsync(product);
            return product; 
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            if (productId is 0)
            {
                return null;
            }

            return await _productRepository.GetProductById(productId);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product.Id is 0)
            {
                return false;
            }

            var updatedProduct = await _productRepository.UpdateProductAsync(product);

            if (!updatedProduct)
            {
                return false;
            }

            return true;
        }
    }
}

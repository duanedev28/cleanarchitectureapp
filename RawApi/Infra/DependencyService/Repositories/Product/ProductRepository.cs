using Microsoft.EntityFrameworkCore;
using RawApi.Domain;
using RawApi.Domain.Entities;

namespace RawApi.Infra
{
    public class ProductRepository(AppDbContext appDbContext) : IProductRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task SaveProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existingProduct = await GetProductById(product.Id);

            if (existingProduct is null)
                return false;

            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }

}

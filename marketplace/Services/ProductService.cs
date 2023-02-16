using marketplace.Data;
using marketplace.Interfaces;
using marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext context;

        public ProductService(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var result = await context.Products
                .AddAsync(product);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products
                .OrderByDescending(x => x.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products
                .AsNoTracking()
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateProduct(Product updatedProduct)
        {
            var product = await GetProductById(updatedProduct.Id);
            if (product is not null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                context.Update(product);
                await context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Product?> DeleteProduct(int id)
        {

            var product = await GetProductById(id);
            if (product is not null)
            {
                product = context.Remove(product).Entity;
                await context.SaveChangesAsync();
            }
            return product;
        }
    }
}

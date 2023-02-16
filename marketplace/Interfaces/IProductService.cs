using marketplace.Models;

namespace marketplace.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> UpdateProduct(Product updatedProduct);
        Task<Product?> DeleteProduct(int id);
    }
}

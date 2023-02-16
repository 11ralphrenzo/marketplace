using marketplace.Interfaces;
using marketplace.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController
    {
        private readonly IProductService productService;
        private readonly ILogger logger;

        public ProductsController(IProductService productService, ILogger logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await this.productService.GetAllProducts();
        }

        [HttpPost]
        public async Task<Product> Get(Product product)
        {
            return await productService.AddProduct(product);
        }

        [HttpPut]
        public async Task<Product?> Update(Product updatedProduct)
        {
            return await productService.UpdateProduct(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<Product?> Delete(int id)
        {
            return await productService.DeleteProduct(id);
        }
    }
}

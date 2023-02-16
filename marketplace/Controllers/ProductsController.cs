using marketplace.Interfaces;
using marketplace.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await this.productService.GetAllProducts());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Get(Product product)
        {
            return Ok(await productService.AddProduct(product));
        }

        [HttpPut]
        public async Task<ActionResult<Product?>> Update(Product updatedProduct)
        {
            return Ok(await productService.UpdateProduct(updatedProduct));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product?>> Delete(int id)
        {
            return Ok(await productService.DeleteProduct(id));
        }
    }
}

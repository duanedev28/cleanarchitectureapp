using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawApi.Application;
using RawApi.Domain.Entities;

namespace RawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpPost(nameof(CreateProductAsync))]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            var productId = await _productService.CreateProductAsync(product);
            return Ok(new { Id = productId, Message = "Product created successfully" });
        }

        [HttpGet(nameof(GetProductByIdAsync))]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Product id should not be null");
            }

            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPut(nameof(UpdateProductAsync))]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Product doesn't exist");
            }

            var isProductUpdated = await _productService.UpdateProductAsync(product);

            if (!isProductUpdated)
            {
                return NotFound("Product doesn't exist");
            }

            return Ok(product);
        }
    }

}

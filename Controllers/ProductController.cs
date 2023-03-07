using CRUD_BAL.Service;
using CRUD_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDAspNetCore5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ProductService ProductService, ILogger<ProductController> logger)
        {
            _productService = ProductService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogInformation("Getting all Product ");
            return await _productService.GetProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
 
            var product = await _productService.GetProducts(id);
            _logger.LogInformation("Product Fetched", product);
            return product == null ? NotFound() : Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducts([FromBody] Product product)
        {
            var newProduct = await _productService.AddProduct(product);
            _logger.LogInformation("Product Added", newProduct);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, [FromBody] Product product)
        {
            _logger.LogInformation("Modifing Product", product);
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProducts(int id)
        {
            _logger.LogInformation("Deleting Product with prodct Id", id);
            await _productService.DeleteProduct(id);
        }
    }
}

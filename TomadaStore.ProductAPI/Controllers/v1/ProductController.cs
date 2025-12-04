using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductRequestDTO>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductRequestDTO product)
        {
            try
            {
                await _productService.CreateProductAsync(product);
                return Created();

                // return CreatedAtAction(nameof(GetByIdProductsAsync(product.Id)), null);   >   Aplica-se para exibir o objeto criado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetCustomerByIdAsync(ObjectId id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding a product.");
                return Problem(ex.Message);
            }
        }



    }
}

using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Repositories.Interfaces;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }


        public async Task CreateProductAsync(ProductRequestDTO productDto)
        {
            try
            {
                await _productRepository.CreateProductAsync(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating producDto: {ex.Message}");
                throw;
            }
        }


        public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving products: {ex.Message}");
                throw;
            }
        }


        public async Task<ProductResponseDTO> GetProductByIdAsync(ObjectId id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);

                if (product is null) throw new KeyNotFoundException("Product not found");

                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task UpdateProductAsync(string id, ProductRequestDTO productDto)
        {

        }

        public async Task DeleteProductAsync(string id)
        {

        }
    }
}

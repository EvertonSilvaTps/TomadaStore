using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;
using TomadaStore.ProductAPI.Repositories.Interfaces;

namespace TomadaStore.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _mongoCollection;

        public ProductRepository(ILogger<ProductRepository> logger,
                                ConnectionDB connection)
        {
            _logger = logger;
            _mongoCollection = connection.GetMongoCollection();
        }

        public async Task CreateProductAsync(ProductRequestDTO productDTO)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(new Product
                (
                    productDTO.Name,
                    productDTO.Description,
                    productDTO.Price,
                    new Category
                    (
                        productDTO.Category.Name,
                        productDTO.Category.Description
                    )
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _mongoCollection.Find(_ => true, null).ToListAsync();
                return products.Select(product => new ProductResponseDTO
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = product.Category.Id.ToString(),
                        Name = product.Category.Name,
                        Description = product.Category.Description
                    }
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("erro" + ex.Message);
                throw;
            }
        }


        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            try
            {
                var product = await _mongoCollection.Find(p => p.Id == new ObjectId(id)).FirstOrDefaultAsync();

                ProductResponseDTO productResponse = new()
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = product.Category.Id.ToString(),
                        Name = product.Category.Name,
                        Description = product.Category.Description
                    }
                };

                return productResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("erro" + ex.Message);
                throw;
            }
        }



    }
}

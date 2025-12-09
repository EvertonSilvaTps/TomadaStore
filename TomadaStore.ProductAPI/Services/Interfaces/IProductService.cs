using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductRequestDTO productDto);

        Task<List<ProductResponseDTO>> GetAllProductsAsync();

        Task<ProductResponseDTO> GetProductByIdAsync(string id);

        Task UpdateProductAsync(string id, ProductRequestDTO productDto);

        Task DeleteProductAsync(string id);
    }
}

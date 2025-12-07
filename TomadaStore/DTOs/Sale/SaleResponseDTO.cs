using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleResponseDTO
    {
        public string Id { get; init; }
        public CustomerResponseDTO Customer { get; init; }
        public List<ProductResponseDTO> Products { get; init; }
        public DateTime saleDate { get; init; }
        public decimal TotalPrice { get; init; }
    }
}

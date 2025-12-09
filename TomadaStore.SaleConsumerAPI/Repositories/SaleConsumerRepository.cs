using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumerAPI.Date;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Repositories
{
    public class SaleConsumerRepository : ISaleConsumerRepository
    {
        private readonly ILogger<SaleConsumerRepository> _logger;
        private readonly IMongoCollection<Sale> _saleCollection;

        public SaleConsumerRepository(ILogger<SaleConsumerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _saleCollection = connection.GetMongoCollection();
        }


        public async Task CreateSaleAsync(CustomerResponseDTO customer, List<ProductResponseDTO> productList)
        {
            try
            {
                var customerSale = new Customer
                (
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    customer.Active
                );

                var products = new List<Product>();

                decimal total = 0;

                foreach (var product in productList)
                {
                    var productSale = new Product
                    (
                        product.Id,
                        product.Name,
                        product.Description,
                        product.Price,
                        new Category(product.Category.Id, product.Category.Name, product.Category.Description)
                    );

                    products.Add(productSale);
                    total += product.Price;
                }

                await _saleCollection.InsertOneAsync(new Sale(customerSale, products, total));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}

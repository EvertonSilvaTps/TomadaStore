using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories.Interfaces;

namespace TomadaStore.SaleAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly IMongoCollection<Sale> _saleCollection;

        // Builder with dependency injection (ILogger; and ConnectionDB)
        public SaleRepository(ILogger<SaleRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _saleCollection = connection.GetMongoCollection();
        }


        public async Task CreateSaleAsync(CustomerResponseDTO customer, List<ProductResponseDTO> productList)
        {
            try
            {
               // Instanciado um objeto 'Customer' já o armazenando aos seus campos os valores deles
                var customerSale = new Customer
                (
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    customer.Active
                );

                var products = new List<Product>();  // Lista de products

                decimal total = 0;  // Variable para armazenar a soma dos prices de produts

                // Para cada laço, será instanciado um objeto para guardar os dados do product e adicioná-lo a list
                // E a variável 'total' guardará/somará o price deste product 
                foreach (var product in productList)
                {
                    // instanciado o objeto 'productSale' e já definindo os valores dos campos
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

                // Executando a venda o armazenando no banco de dados MongoDB
                await _saleCollection.InsertOneAsync(new Sale (customerSale, products, total));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}

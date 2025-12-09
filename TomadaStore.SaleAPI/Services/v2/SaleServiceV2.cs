using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Services.v1;
using TomadaStore.SaleAPI.Services.v2.Interfaces;

namespace TomadaStore.SaleAPI.Services.v2
{
    public class SaleServiceV2 : ISaleServiceV2
    {
        private readonly ILogger<SaleService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectionFactory _connectionFactory;


        public SaleServiceV2(ILogger<SaleService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _connectionFactory = new ConnectionFactory { HostName = "localhost" };
        }


        public async Task CreateSaleAsync(int idCustomer, List<string> idsProduct)
        {
            try
            {
                // Costumer

                var httpClientCustomer = _httpClientFactory.CreateClient("Customer");

                var customer = await httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

                var customerSale = new Customer
                (
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    customer.Active
                );

                // Product

                var httpClientProduct = _httpClientFactory.CreateClient("Product");

                // List of products
                var products = new List<Product>();
                decimal total = 0;

                foreach (var idProduct in idsProduct)
                {
                    var product = await httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);


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

                // Validação SeExiste Customer or Products
                if ((customer is null) || (products.Any(p => p is null)))
                    throw new Exception("Foi feita referencia de Ids inexistentes");

                // Created of Sale
                Sale sale = new Sale(customerSale, products, total);


                // Insert in the Fila
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "sale", durable: false,
                                                                exclusive: false,
                                                                autoDelete: false,
                                                                arguments: null);

                var saleString = JsonSerializer.Serialize<Sale>(sale);
                var saleJson = Encoding.UTF8.GetBytes(saleString);
                
                await channel.BasicPublishAsync(exchange: string.Empty,
                                                routingKey: "sale",
                                                body: saleJson);

                _logger.LogInformation(" [x] Sent {0}", sale);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao criar uma venda.: {ex.Message}");
                throw;
            }
        }

    }
}

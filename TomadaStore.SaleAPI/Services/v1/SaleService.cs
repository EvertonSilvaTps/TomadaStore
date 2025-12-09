using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Services.v1
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

            private readonly ILogger<SaleService> _logger;

            private readonly IHttpClientFactory _httpClientFactory;

        //private readonly HttpClient _httpClientProduct;
        //private readonly HttpClient _httpClientCustomer;


        // Builder with dependency injection (Interface Repository; ILogger; HttpClient of Product; and HttpClient of Customer)
        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger, IHttpClientFactory httpClientFactory)
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
            


        public async Task CreateSaleAsync(int idCustomer, List<string> idsProduct)
        {
            try
            {
                var httpClientCustomer = _httpClientFactory.CreateClient("Customer");


                // Dado Id informado, converte os dados no formato JSON pra objeto C# do type (<CustomerResponseDTO>) com base aos campos da class 
                var customer = await httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

                // Dado Id informado, converte os dados do formato JSON pra objeto C# do type (<ProductResponseDTO>) com base aos campos da class 
                //var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idsProduct);

                var httpClientProduct = _httpClientFactory.CreateClient("Produtc");
                var products = new List<ProductResponseDTO>(); // List of products of the ProductResponseDTO type

                foreach (var idProduct in idsProduct)
                {
                    // instancia um objeto do type ProductResponseDTO pra cada id da lista de produts,
                    // convertendo os dados do formato JSON pra objeto C# com base aos campos da class
                    var product = await httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);

                    // adiciona o product a lista de products do type List<ProductResponseDTO>
                    products.Add(product);
                }


                // Se não existir o cliente do Id informado OU não haver nenhum produto na lista de produtos, a venda é encerrada
                if ((customer is null) || (products.Any(p => p is null)))
                    throw new Exception("Foi feita referencia de Ids inexistentes");


                // Sale created
                await _saleRepository.CreateSaleAsync(customer, products);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating a sale: {ex.Message}");
                throw;
            }
        }
    }
}

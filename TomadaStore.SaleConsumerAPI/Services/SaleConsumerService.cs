using TomadaStore.SaleConsumerAPI.Repositories;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;
using TomadaStore.SaleConsumerAPI.Services.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Services
{
    public class SaleConsumerService : ISaleConsumerService
    {
		private readonly ILogger<SaleConsumerRepository> _logger;
		private readonly ISaleConsumerRepository _saleConsumerRepository;

        public SaleConsumerService(ILogger<SaleConsumerRepository> logger, ISaleConsumerRepository saleConsumerRepository)
        {
            _logger = logger;
            _saleConsumerRepository = saleConsumerRepository;
        }


        public async Task CreateSaleAsync(int idCustomer, List<string> idsProduct)
        {
			try
			{




			}
			catch (Exception e)
			{

				throw;
			}
        }



    }
}

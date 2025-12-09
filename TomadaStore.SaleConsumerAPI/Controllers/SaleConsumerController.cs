using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleConsumerAPI.Services.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleConsumerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISaleConsumerService _saleConsumerService;


        [HttpPost("{idCustomer}")]
        public async Task<IActionResult> CreateSaleAsync(int customer, [FromBody] List<string> productList)
        {
            try
            {
                await _saleConsumerService.CreateSaleAsync(customer, productList);

                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao criar a venda.");
                return Problem(e.Message);
            }
        }

    }
}

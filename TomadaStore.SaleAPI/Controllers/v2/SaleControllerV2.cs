using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleAPI.Services.v2.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SaleControllerV2 : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISaleServiceV2 _saleService;

        public SaleControllerV2(ILogger logger, ISaleServiceV2 saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }


        [HttpPost("customer/{idCustomer}")]
        public async Task<IActionResult> CreateSaleAsync(int idCustomer, [FromBody] List<string> productsIds)
        {
            try
            {
                await _saleService.CreateSaleAsync(idCustomer, productsIds);
                
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar a venda");
                return Problem(ex.Message);
            }
        }









    }
}

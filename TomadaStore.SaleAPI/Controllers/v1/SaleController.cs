using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;  // propriedade que vai estender as funções da classe Service


        // Builder: Injeção de depêndencia
        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }


        // Rota para Insert de venda:  Sale/customer/idCustomer/product/idProduct
        [HttpPost("customer/{idCustomer}")]
        public async Task<IActionResult> CreateSaleAsync(int idCustomer, [FromBody] List<string> productIds)
        {
            try
            {
                await _saleService.CreateSaleAsync(idCustomer, productIds);

                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error ocurred while creating sale.");
                return Problem(e.Message);
            }


        }

    }
}

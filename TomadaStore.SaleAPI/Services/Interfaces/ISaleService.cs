using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync(int idCustomer, List<string> idsProduct);

    }
}

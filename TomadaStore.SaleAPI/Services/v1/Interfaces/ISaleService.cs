using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Services.v1.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync(int idCustomer, List<string> idsProduct);

    }
}

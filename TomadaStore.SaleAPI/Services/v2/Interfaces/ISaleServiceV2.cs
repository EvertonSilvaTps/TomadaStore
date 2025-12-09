namespace TomadaStore.SaleAPI.Services.v2.Interfaces
{
    public interface ISaleServiceV2
    {
        Task CreateSaleAsync(int idCustomer, List<string> idsProduct);
    }
}

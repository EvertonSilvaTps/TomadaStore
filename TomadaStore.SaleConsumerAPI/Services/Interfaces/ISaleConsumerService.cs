namespace TomadaStore.SaleConsumerAPI.Services.Interfaces
{
    public interface ISaleConsumerService
    {
        Task CreateSaleAsync(int idCustomer, List<string> idsProduct);


    }
}

using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadasSore.CustomerAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);

        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);

        Task DeleteCustomerByIdAsync(int id);
    }
}

using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadasSore.CustomerAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        
        Task InsertCustomerAsync(CustomerRequestDTO customer);

        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);

        Task DeleteCustomerByIdAsync(int id);

    }
}

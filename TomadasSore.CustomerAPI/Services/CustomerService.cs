using Microsoft.AspNetCore.Http.HttpResults;
using TomadasSore.CustomerAPI.Repository;
using TomadasSore.CustomerAPI.Repository.Interfaces;
using TomadasSore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadasSore.CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                await _customerRepository.InsertCustomerAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllCustomersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer is null) throw new KeyNotFoundException("Customer not found.");

                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var customerFound = await _customerRepository.GetCustomerByIdAsync(id);
                if (customerFound is null) throw new KeyNotFoundException("Customer noy found.");

                await _customerRepository.DeleteCustomerByIdAsync(id);
            }
            catch(Exception e)
            {
                throw;
            }


        }


    }
}

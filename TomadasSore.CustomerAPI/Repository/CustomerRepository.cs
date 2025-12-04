using Dapper;
using Microsoft.Data.SqlClient;
using TomadasSore.CustomerAPI.Data;
using TomadasSore.CustomerAPI.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;

namespace TomadasSore.CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }



        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var sqlSelect = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Active 
                                    FROM Customers";

                var customers = await _connection.QueryAsync<CustomerResponseDTO>(sqlSelect);

                return customers.ToList();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error inserting customr: {sqlEx.Message}");

                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }



        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Active) 
                                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Active)";

                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber, customer.Active});
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error inserting customr: {sqlEx.Message}");

                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");
            }
        }


        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sqlSelectById = @"SELECT Id, FirstName, LastName, Email, PhoneNumber, Active 
                                    FROM Customers WHERE Id = @Id";

                var customer = await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sqlSelectById, new { Id = id });

                return customer!;
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error found customer: {sqlEx.Message}");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error found customer: {ex.Message}");
                throw new Exception(ex.StackTrace);
            }
        }


        public async Task DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var sqlDeleteById = @"UPDATE Customers SET Active = 0 WHERE Id = @Id";

                var customerUpdate = await _connection.ExecuteAsync(sqlDeleteById, new { Id = id });
            }
            catch (SqlException e)
            {
                _logger.LogError($"SQL Error deleted customer: {e.StackTrace}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleted customer: {ex.Message}");
            }

        }




    }
}

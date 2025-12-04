using Microsoft.Extensions.Configuration;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
    internal class SQLDBConnectionImpl : IDBConnection    // Inheriting the method ConnectionString()
    {
        private readonly string _connectionString;       // string type property
        private readonly IConfiguration configuration;   // IConfiguration type property

        public SQLDBConnectionImpl()  // Builder who receives my string connection
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public string ConnectionString()  // return my string
        {
            return _connectionString;
        }
    }
}
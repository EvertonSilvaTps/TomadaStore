using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{

    // Database connection factory
    public abstract class DBConnectionFactory
    {
        public abstract IDBConnection CreateDBConnection();  // herdando do IDBConnection

        public string GetConnectionString()
        {
            var dbConnection = CreateDBConnection();  // return do type IDBConnection
            return dbConnection.ConnectionString();  // Now he will return do type string
        }   

    }
}

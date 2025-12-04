using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{

    // Here I create the objects of database SQL
    public class SQLDBConnection : DBConnectionFactory
    {
        public override IDBConnection CreateDBConnection()  // Override of CreateDBConnection to create SQL type objects
        {
            return new SQLDBConnectionImpl(); // return object
        }


    }
}

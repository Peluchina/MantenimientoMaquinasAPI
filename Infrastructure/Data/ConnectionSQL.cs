using Microsoft.Data.SqlClient;
using System.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Data
{
    public class ConnectionSqL:IConnectionSQL
    {
        private readonly string _connectionString;

        public ConnectionSqL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PPA")
                                ?? throw new InvalidOperationException("La cadena de conexión 'PPA' no se encuentra configurada en appsettings.json.");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

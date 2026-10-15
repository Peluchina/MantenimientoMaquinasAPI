using System.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Data
{
    public interface IConnectionSQL
    {
        IDbConnection GetConnection();
    }
}

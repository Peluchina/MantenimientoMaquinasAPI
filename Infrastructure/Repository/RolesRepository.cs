using Dapper;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Connections;
using System.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository
{
    public class RolesRepository(IConnectionSQL connectionFactory) : IRolesRespositoryWR
    {
        public async Task<IEnumerable<Roles>> GetAll(CancellationToken cancellationToken = default)
        {
            using var connection = connectionFactory.GetConnection();
            var result = await connection.QueryAsync<Roles>(new CommandDefinition(
                "dbo.Roles_GetAll", commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            return result.AsList();
        }

    }
}

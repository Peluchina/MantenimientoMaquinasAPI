using System.Data;
using Dapper;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository;

public sealed class MachineTemperatureHistoryRepository(IConnectionSQL connectionFactory)
    : IMachineTemperatureHistoryRespositoryWR
{
    public async Task<IEnumerable<MachineTemperatureHistory>> GetByMachine(
        int machineId, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        var history = await connection.QueryAsync<MachineTemperatureHistory>(new CommandDefinition(
            "dbo.MachineTemperatureHistory_GetByMachine", new { MachineId = machineId },
            commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
        return history.AsList();
    }

    public async Task<long> Insert(MachineTemperatureHistory history, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleAsync<long>(new CommandDefinition("dbo.MachineTemperatureHistory_Create",
            new { history.MachineId, history.UserId, history.Temperature },
            commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }
}

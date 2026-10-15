using System.Data;
using Dapper;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository;

public sealed class MaintenanceRecordsRepository(IConnectionSQL connectionFactory)
    : IMaintenanceRecordsRespositoryWR
{
    public async Task<IEnumerable<MaintenanceRecords>> GetByMachine(
        int machineId, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        var records = await connection.QueryAsync<MaintenanceRecords>(new CommandDefinition(
            "dbo.MaintenanceRecords_GetByMachine", new { MachineId = machineId },
            commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
        return records.AsList();
    }

    public async Task<int> Insert(MaintenanceRecords record, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleAsync<int>(new CommandDefinition("dbo.MaintenanceRecords_Create",
            new { record.MachineId, record.UserId, record.MaintenanceDate, record.MaintenanceType,
                record.Description, record.Cost }, commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken));
    }
}

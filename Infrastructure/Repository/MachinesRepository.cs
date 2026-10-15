using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository;

public sealed class MachinesRepository(IConnectionSQL connectionFactory) : IMachinesRespositoryWR
{
    public async Task<IEnumerable<Machines>> GetAll(CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        var result = await connection.QueryAsync<Machines>(new CommandDefinition(
            "dbo.Machines_GetAll", commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
        return result.AsList();
    }

    public async Task<Machines?> GetById(long id, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<Machines>(new CommandDefinition(
            "dbo.Machines_GetById", new { MachineId = id }, commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken));
    }

    public async Task<int> Insert(Machines machine, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleAsync<int>(new CommandDefinition("dbo.Machines_Create",
            new { machine.MachineName, machine.Brand, machine.CurrentTemperature, machine.MaxTemperature,
                machine.MachineStatus, machine.LastMaintenanceDate }, commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken));
    }

    public async Task<bool> Update(Machines machine, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        try
        {
            await connection.ExecuteAsync(new CommandDefinition("dbo.Machines_Update",
                new { machine.MachineId, machine.MachineName, machine.Brand, machine.CurrentTemperature,
                    machine.MaxTemperature, machine.MachineStatus, machine.LastMaintenanceDate },
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            return true;
        }
        catch (SqlException exception) when (exception.Number == 50005) { return false; }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        try
        {
            await connection.ExecuteAsync(new CommandDefinition("dbo.Machines_Delete", new { MachineId = id },
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            return true;
        }
        catch (SqlException exception) when (exception.Number == 50006) { return false; }
        catch (SqlException exception) when (exception.Number is 50007 or 50008)
        { throw new MachineHasRelatedRecordsException(id, exception); }
    }
}

public sealed class MachineHasRelatedRecordsException(int machineId, Exception innerException)
    : Exception($"La máquina {machineId} tiene registros asociados y no puede eliminarse.", innerException);

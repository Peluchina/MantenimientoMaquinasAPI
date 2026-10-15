using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MachinesGetQueryHandler(IMachinesRespositoryWR repository)
{
    public async Task<MachinesGetResponse?> Handle(MachinesGetQuery query, CancellationToken ct = default)
    {
        if (query.Params.MachineId <= 0) return null;
        var machine = await repository.GetById(query.Params.MachineId, ct);
        return machine is null ? null : new MachinesGetResponse
        {
            MachineId = machine.MachineId,
            MachineName = machine.MachineName,
            Brand = machine.Brand,
            CurrentTemperature = machine.CurrentTemperature,
            MaxTemperature = machine.MaxTemperature,
            LastMaintenanceDate = machine.LastMaintenanceDate,
            MachineStatus = machine.MachineStatus
        };
    }
}

using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MachinesListQueryHandler(IMachinesRespositoryWR repository)
{
    public async Task<IReadOnlyList<MachinesListResponse>> Handle(MachinesListQuery query, CancellationToken ct = default)
    {
        var machines = await repository.GetAll(ct);
        return machines.Select(machine => new MachinesListResponse
        {
            MachineId = machine.MachineId,
            MachineName = machine.MachineName,
            Brand = machine.Brand,
            CurrentTemperature = machine.CurrentTemperature,
            MaxTemperature = machine.MaxTemperature,
            LastMaintenanceDate = machine.LastMaintenanceDate,
            MachineStatus = machine.MachineStatus
        }).ToArray();
    }
}

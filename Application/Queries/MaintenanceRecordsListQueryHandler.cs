using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MaintenanceRecordsListQueryHandler(IMaintenanceRecordsRespositoryWR repository)
{
    public async Task<IReadOnlyList<MaintenanceRecordsGetResponse>> Handle(
        MaintenanceRecordsListQuery query, CancellationToken ct = default)
    {
        var records = await repository.GetByMachine(query.Params.MachineId, ct);
        return records.Select(MaintenanceRecordsGetQueryHandler.ToResponse).ToArray();
    }
}

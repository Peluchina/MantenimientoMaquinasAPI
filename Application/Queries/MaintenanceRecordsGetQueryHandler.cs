using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MaintenanceRecordsGetQueryHandler(IMaintenanceRecordsRespositoryWR repository)
{
    public async Task<IReadOnlyList<MaintenanceRecordsGetResponse>> Handle(
        MaintenanceRecordsGetQuery query, CancellationToken ct = default)
    {
        var records = await repository.GetByMachine(query.Params.MachineId, ct);
        return records.Select(ToResponse).ToArray();
    }

    internal static MaintenanceRecordsGetResponse ToResponse(Domain.Agregates.MaintenanceRecords record) => new()
    {
        MaintenanceId = record.MaintenanceId,
        MachineId = record.MachineId,
        UserId = record.UserId,
        MachineName = record.MachineName,
        Responsible = record.Responsible,
        MaintenanceDate = record.MaintenanceDate,
        MaintenanceType = record.MaintenanceType,
        Description = record.Description,
        Cost = record.Cost
    };
}

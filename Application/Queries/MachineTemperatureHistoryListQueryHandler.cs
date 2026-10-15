using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MachineTemperatureHistoryListQueryHandler(IMachineTemperatureHistoryRespositoryWR repository)
{
    public async Task<IReadOnlyList<MachineTemperatureHistoryGetResponse>> Handle(
        MachineTemperatureHistoryListQuery query, CancellationToken ct = default)
    {
        var rows = await repository.GetByMachine(query.Params.MachineId, ct);
        return rows.Select(row => new MachineTemperatureHistoryGetResponse
        {
            TemperatureHistoryId = row.TemperatureHistoryId,
            MachineId = row.MachineId,
            UserId = row.UserId,
            Temperature = row.Temperature,
            RecordedAt = row.RecordedAt
        }).ToArray();
    }
}

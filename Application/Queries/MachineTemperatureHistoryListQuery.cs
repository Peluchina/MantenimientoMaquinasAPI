using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MachineTemperatureHistoryListQuery
{
    public required MachineTemperatureHistoryGetParams Params { get; init; }
}

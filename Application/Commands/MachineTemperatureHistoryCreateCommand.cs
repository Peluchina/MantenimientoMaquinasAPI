using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MachineTemperatureHistoryCreateCommand
{
    public required MachineTemperatureHistoryGetParams Params { get; init; }
    public required MachineTemperatureHistoryCreateRequest Request { get; init; }
}

using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Mapping;

public static class MachineTemperatureHistoryCreateCommandMapping
{
    public static MachineTemperatureHistory ToEntity(MachineTemperatureHistoryCreateCommand command) => new()
    {
        MachineId = command.Params.MachineId,
        UserId = command.Request.UserId,
        Temperature = command.Request.Temperature
    };
}

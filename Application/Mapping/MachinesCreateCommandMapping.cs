using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Mapping;

public static class MachinesCreateCommandMapping
{
    public static Machines ToEntity(MachinesCreateCommand command) => new()
    {
        MachineName = command.Request.MachineName?.Trim() ?? string.Empty,
        Brand = command.Request.Brand?.Trim() ?? string.Empty,
        CurrentTemperature = command.Request.CurrentTemperature,
        MaxTemperature = command.Request.MaxTemperature,
        MachineStatus = string.IsNullOrWhiteSpace(command.Request.MachineStatus)
            ? "OPERATIVA" : command.Request.MachineStatus.Trim().ToUpperInvariant(),
        LastMaintenanceDate = command.Request.LastMaintenanceDate
    };
}

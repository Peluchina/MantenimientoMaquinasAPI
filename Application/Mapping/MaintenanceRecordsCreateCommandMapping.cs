using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Mapping;

public static class MaintenanceRecordsCreateCommandMapping
{
    public static MaintenanceRecords ToEntity(MaintenanceRecordsCreateCommand command) => new()
    {
        MachineId = command.Params.MachineId,
        UserId = command.Request.UserId,
        MaintenanceDate = command.Request.MaintenanceDate,
        MaintenanceType = command.Request.MaintenanceType?.Trim(),
        Description = command.Request.Description?.Trim(),
        Cost = command.Request.Cost
    };
}

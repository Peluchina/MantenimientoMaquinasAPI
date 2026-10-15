using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MaintenanceRecordsCreateCommand
{
    public required MaintenanceRecordsGetParams Params { get; init; }
    public required MaintenanceRecordsCreateRequest Request { get; init; }
}

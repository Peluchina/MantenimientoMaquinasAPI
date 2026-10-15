using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class MaintenanceRecordsListQuery
{
    public required MaintenanceRecordsGetParams Params { get; init; }
}

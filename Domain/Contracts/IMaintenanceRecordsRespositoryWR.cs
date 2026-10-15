using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Domain.Contracts;

public interface IMaintenanceRecordsRespositoryWR
{
    Task<IEnumerable<MaintenanceRecords>> GetByMachine(int machineId, CancellationToken cancellationToken = default);
    Task<int> Insert(MaintenanceRecords maintenanceRecord, CancellationToken cancellationToken = default);
}

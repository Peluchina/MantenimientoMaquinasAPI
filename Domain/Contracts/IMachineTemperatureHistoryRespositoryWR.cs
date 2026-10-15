using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Domain.Contracts;

public interface IMachineTemperatureHistoryRespositoryWR
{
    Task<IEnumerable<MachineTemperatureHistory>> GetByMachine(int machineId, CancellationToken cancellationToken = default);
    Task<long> Insert(MachineTemperatureHistory history, CancellationToken cancellationToken = default);
}

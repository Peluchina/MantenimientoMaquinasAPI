using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Domain.Contracts
{
    public interface IMachinesRespositoryWR
    {
        Task<IEnumerable<Machines>> GetAll(CancellationToken cancellationToken = default);
        Task<Machines?> GetById(long id, CancellationToken cancellationToken = default);
        Task<int> Insert(Machines machines, CancellationToken cancellationToken = default);
        Task<bool> Update(Machines machines, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}

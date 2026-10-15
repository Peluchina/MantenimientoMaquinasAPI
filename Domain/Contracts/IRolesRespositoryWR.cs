using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Domain.Contracts
{
    public interface IRolesRespositoryWR
    {
        Task<IEnumerable<Roles>> GetAll(CancellationToken cancellationToken = default);
    }
}

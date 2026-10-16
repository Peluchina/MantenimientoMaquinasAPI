using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Domain.Contracts;

public interface IUsersRepositoryWR
{
    Task<IEnumerable<Users>> GetAll(CancellationToken cancellationToken = default);
    Task<Users?> GetById(int id, CancellationToken cancellationToken = default);
    Task<Users?> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<int> Insert(Users user, CancellationToken cancellationToken = default);
    Task<bool> Update(Users user, CancellationToken cancellationToken = default);
    Task<bool> Deactivate(int id, CancellationToken cancellationToken = default);
}

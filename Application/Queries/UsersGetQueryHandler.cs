using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class UsersGetQueryHandler(IUsersRepositoryWR repository)
{
    public async Task<UsersGetResponse?> Handle(UsersGetQuery query, CancellationToken ct = default)
    {
        if (query.Params.UserId <= 0) return null;
        var user = await repository.GetById(query.Params.UserId, ct);
        return user is null ? null : new UsersGetResponse
        {
            UserId = user.UserId,
            RoleId = user.RoleId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserStatus = user.UserStatus,
            RoleName = user.RoleName,
            CreatedAt = user.CreatedAt
        };
    }
}

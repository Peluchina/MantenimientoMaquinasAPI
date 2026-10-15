using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Queries;

public sealed class UsersListQueryHandler(IUsersRepositoryWR repository)
{
    public async Task<IReadOnlyList<UsersListResponse>> Handle(UsersListQuery query, CancellationToken ct = default)
    {
        var users = await repository.GetAll(ct);
        return users.Select(user => new UsersListResponse
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
        }).ToArray();
    }
}

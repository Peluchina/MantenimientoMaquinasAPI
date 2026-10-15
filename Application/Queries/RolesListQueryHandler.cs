using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Domain.Contracts;
using System.Data;

namespace MantenimientoMaquinasAPI.Application.Queries
{
    public class RolesListQueryHandler(IRolesRespositoryWR repository)
    {
        public async Task<IReadOnlyList<RolesListResponse>> Handle(RolesListQuery query, CancellationToken ct = default)
        {
            var roles = await repository.GetAll(ct);
            return roles.Select(role => new RolesListResponse
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description
            }).ToArray();
        }
    }
}

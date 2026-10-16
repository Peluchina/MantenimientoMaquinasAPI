using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Users users);
    }
}

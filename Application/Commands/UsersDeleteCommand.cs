using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class UsersDeleteCommand
{
    public required UsersGetParams Params { get; init; }
}

using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class UsersUpdateStatusCommand
    {
        public required UsersUpdateStatusParams Params { get; set; }
        public required UsersUpdateStatusRequest Request { get; set; }
    }
}

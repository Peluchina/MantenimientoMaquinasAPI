using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class UsersUpdateCommand
    {
        public required UsersUpdateParams Params { get; set; }
        public required UsersUpdateRequest Request { get; set; }
    }
}

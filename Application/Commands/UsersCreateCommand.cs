using MantenimientoMaquinasAPI.Application.Dto;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class UsersCreateCommand
    {
        public required UsersCreateRequest Request { get; set; }
    }
}

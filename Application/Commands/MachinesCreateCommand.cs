using MantenimientoMaquinasAPI.Application.Dto;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MachinesCreateCommand
    {
        public required MachinesCreateRequest Request { get; set;}
    }
}

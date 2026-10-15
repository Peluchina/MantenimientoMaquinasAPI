using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MachinesUpdateStatusCommand
    {
        public required MachinesUpdateStatusParams Params { get; set; }
        public required MachinesUpdateRequest Request { get; set; }
    }
}

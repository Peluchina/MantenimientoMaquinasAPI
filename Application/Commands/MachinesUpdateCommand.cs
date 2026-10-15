using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MachinesUpdateCommand
    {
        public required MachinesUpdateParams Params { get; set; }
        public required MachinesUpdateRequest Request { get; set; }
    }
}

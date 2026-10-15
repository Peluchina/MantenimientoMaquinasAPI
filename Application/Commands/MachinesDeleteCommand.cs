using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MachinesDeleteCommand
    {
        public required MachinesDeleteParams Params { get; set; }
    }
}

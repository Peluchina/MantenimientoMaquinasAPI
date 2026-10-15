using MantenimientoMaquinasAPI.Application.Dto;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MachineTemperatureHistoryCreateCommand
    {
        public required MachineTemperatureHistoryCreateRequest Request { get; set; }
    }
}

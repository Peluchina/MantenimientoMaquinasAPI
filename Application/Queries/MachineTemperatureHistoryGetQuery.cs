using MantenimientoMaquinasAPI.Application.Parameters;

namespace MantenimientoMaquinasAPI.Application.Queries
{
    public class MachineTemperatureHistoryGetQuery
    {
        public required MachineTemperatureHistoryGetParams Params { get; set; }
    }
}

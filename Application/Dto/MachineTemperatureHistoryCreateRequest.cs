using System.Security.Principal;

namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class MachineTemperatureHistoryCreateRequest
    {
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public decimal Temperature { get; set; }

    }
}

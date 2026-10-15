using MantenimientoMaquinasAPI.Application.Dto;

namespace MantenimientoMaquinasAPI.Application.Commands
{
    public class MaintenanceRecordsCreateCommand
    {
        public required MaintenanceRecordsCreateRequest Request { get; set; }
    }
}

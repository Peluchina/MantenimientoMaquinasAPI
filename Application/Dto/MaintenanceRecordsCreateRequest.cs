namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class MaintenanceRecordsCreateRequest
    {
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? MaintenanceType { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
    }
}

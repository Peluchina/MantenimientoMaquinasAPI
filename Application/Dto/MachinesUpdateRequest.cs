namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class MachinesUpdateRequest
    {
        public string? MachineName { get; set; }
        public string? Brand { get; set; }
        public decimal CurrentTemperature { get; set; }
        public decimal MaxTemperature { get; set; }
        public string? MachineStatus { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
    }
}

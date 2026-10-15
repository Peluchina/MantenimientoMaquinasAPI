namespace MantenimientoMaquinasAPI.Domain.Agregates
{
    public class Machines
    {
        public const decimal MaximumSupportedTemperature = 9999.99m;
        public static readonly IReadOnlySet<string> AllowedStatuses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "OPERATIVA", "MANTENIMIENTO", "INACTIVA", "FALLA"
        };

        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal CurrentTemperature { get; set; }
        public decimal MaxTemperature { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public string MachineStatus { get; set; } = "OPERATIVA";

    }
}

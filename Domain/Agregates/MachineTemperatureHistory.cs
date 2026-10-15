namespace MantenimientoMaquinasAPI.Domain.Agregates
{
    public class MachineTemperatureHistory
    {
        public int TemperatureHistoryId { get; set; }
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public decimal Temperature { get; set; }
    }
}

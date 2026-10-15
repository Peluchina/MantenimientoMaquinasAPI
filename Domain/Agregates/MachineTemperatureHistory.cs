namespace MantenimientoMaquinasAPI.Domain.Agregates
{
    public class MachineTemperatureHistory
    {
        public long TemperatureHistoryId { get; set; }
        public int MachineId { get; set; }
        public int? UserId { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}

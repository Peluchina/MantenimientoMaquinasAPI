namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class MachineTemperatureHistoryGetResponse
    {
        public long TemperatureHistoryId { get; set; }
        public int MachineId { get; set; }
        public int? UserId { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}

namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class MachineTemperatureHistoryGetResponse
    {
        public int TemperatureHistoryId { get; set; }
        public int MachineId { get; set; }
        public int UserId { get; set; }
        public decimal Temperature { get; set; }
    }
}

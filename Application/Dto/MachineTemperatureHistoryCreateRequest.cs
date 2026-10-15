namespace MantenimientoMaquinasAPI.Application.Dto;

public sealed class MachineTemperatureHistoryCreateRequest
{
    public int? UserId { get; set; }
    public decimal Temperature { get; set; }
}

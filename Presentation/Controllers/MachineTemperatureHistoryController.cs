using Microsoft.AspNetCore.Mvc;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;
using MantenimientoMaquinasAPI.Application.Queries;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/machines/{machineId:int}/temperatures")]
public sealed class MachineTemperatureHistoryController(
    MachinesGetQueryHandler machines,
    MachineTemperatureHistoryListQueryHandler listHandler,
    MachineTemperatureHistoryCreateCommandHandler createHandler) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MachineTemperatureHistoryGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByMachine(int machineId, CancellationToken ct)
    {
        var machine = await machines.Handle(new MachinesGetQuery
        {
            Params = new MachinesGetParams { MachineId = machineId }
        }, ct);
        if (machine is null) return NotFound();

        var history = await listHandler.Handle(new MachineTemperatureHistoryListQuery
        {
            Params = new MachineTemperatureHistoryGetParams { MachineId = machineId }
        }, ct);
        return Ok(history);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MachineTemperatureHistoryCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(int machineId,
        [FromBody] MachineTemperatureHistoryCreateRequest request, CancellationToken ct)
    {
        var created = await createHandler.Create(new MachineTemperatureHistoryCreateCommand
        {
            Params = new MachineTemperatureHistoryGetParams { MachineId = machineId },
            Request = request
        }, ct);
        return StatusCode(StatusCodes.Status201Created, created);
    }
}

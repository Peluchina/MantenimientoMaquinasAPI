using Microsoft.AspNetCore.Mvc;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;
using MantenimientoMaquinasAPI.Application.Queries;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/machines/{machineId:int}/maintenance-records")]
public sealed class MaintenanceRecordsController(
    MachinesGetQueryHandler machines,
    MaintenanceRecordsListQueryHandler listHandler,
    MaintenanceRecordsCreateCommandHandler createHandler) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MaintenanceRecordsGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByMachine(int machineId, CancellationToken ct)
    {
        var machine = await machines.Handle(new MachinesGetQuery
        {
            Params = new MachinesGetParams { MachineId = machineId }
        }, ct);
        if (machine is null) return NotFound();

        var records = await listHandler.Handle(new MaintenanceRecordsListQuery
        {
            Params = new MaintenanceRecordsGetParams { MachineId = machineId }
        }, ct);
        return Ok(records);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceRecordsCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(int machineId,
        [FromBody] MaintenanceRecordsCreateRequest request, CancellationToken ct)
    {
        var created = await createHandler.Create(new MaintenanceRecordsCreateCommand
        {
            Params = new MaintenanceRecordsGetParams { MachineId = machineId },
            Request = request
        }, ct);
        return StatusCode(StatusCodes.Status201Created, created);
    }
}

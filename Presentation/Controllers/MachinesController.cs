using Microsoft.AspNetCore.Mvc;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;
using MantenimientoMaquinasAPI.Application.Queries;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/machines")]
public sealed class MachinesController(
    MachinesListQueryHandler listHandler,
    MachinesGetQueryHandler getHandler,
    MachinesCreateCommandHandler createHandler,
    MachinesUpdateCommandHandler updateHandler,
    MachinesDeleteCommandHandler deleteHandler) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MachinesListResponse>), StatusCodes.Status200OK)]
    public Task<IReadOnlyList<MachinesListResponse>> GetAll(CancellationToken ct) =>
        listHandler.Handle(new MachinesListQuery(), ct);

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MachinesGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var machine = await getHandler.Handle(new MachinesGetQuery
        {
            Params = new MachinesGetParams { MachineId = id }
        }, ct);
        return machine is null ? NotFound() : Ok(machine);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MachinesCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] MachinesCreateRequest request, CancellationToken ct)
    {
        var created = await createHandler.Create(new MachinesCreateCommand { Request = request }, ct);
        var machine = await getHandler.Handle(new MachinesGetQuery
        {
            Params = new MachinesGetParams { MachineId = created.MachineId }
        }, ct);
        object response = machine is null ? created : machine;
        return CreatedAtAction(nameof(GetById), new { id = created.MachineId }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] MachinesUpdateRequest request, CancellationToken ct)
    {
        var updated = await updateHandler.Update(new MachinesUpdateCommand
        {
            Params = new MachinesUpdateParams { MachineId = id },
            Request = request
        }, ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await deleteHandler.Delete(new MachinesDeleteCommand
        {
            Params = new MachinesDeleteParams { MachineId = id }
        }, ct);
        return deleted ? NoContent() : NotFound();
    }
}

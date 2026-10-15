using Microsoft.AspNetCore.Mvc;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Parameters;
using MantenimientoMaquinasAPI.Application.Queries;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController(
    UsersListQueryHandler listHandler,
    UsersGetQueryHandler getHandler,
    UsersCreateCommandHandler createHandler,
    UsersUpdateCommandHandler updateHandler,
    UsersDeleteCommandHandler deleteHandler) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UsersListResponse>), StatusCodes.Status200OK)]
    public Task<IReadOnlyList<UsersListResponse>> GetAll(CancellationToken ct) =>
        listHandler.Handle(new UsersListQuery(), ct);

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UsersGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var user = await getHandler.Handle(new UsersGetQuery
        {
            Params = new UsersGetParams { UserId = id }
        }, ct);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsersGetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] UsersCreateRequest request, CancellationToken ct)
    {
        var created = await createHandler.Create(new UsersCreateCommand { Request = request }, ct);
        var user = await getHandler.Handle(new UsersGetQuery
        {
            Params = new UsersGetParams { UserId = created.UserId }
        }, ct);
        object response = user is null ? created : user;
        return CreatedAtAction(nameof(GetById), new { id = created.UserId }, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(int id, [FromBody] UsersUpdateRequest request, CancellationToken ct)
    {
        var updated = await updateHandler.Update(new UsersUpdateCommand
        {
            Params = new UsersUpdateParams { UserId = id },
            Request = request
        }, ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deactivated = await deleteHandler.Delete(new UsersDeleteCommand
        {
            Params = new UsersGetParams { UserId = id }
        }, ct);
        return deactivated ? NoContent() : NotFound();
    }
}

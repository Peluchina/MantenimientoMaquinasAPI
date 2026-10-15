using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController(RolesListQueryHandler listHandler) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<RolesListResponse>), StatusCodes.Status200OK)]
        public Task<IReadOnlyList<RolesListResponse>> GetAll(CancellationToken ct) =>
        listHandler.Handle(new RolesListQuery(), ct);







    }
}

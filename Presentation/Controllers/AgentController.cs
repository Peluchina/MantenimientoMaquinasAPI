using MantenimientoMaquinasAPI.Application.Agents;
using Microsoft.AspNetCore.Authorization;
using MantenimientoMaquinasAPI.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/agent")]
[Authorize]
public class AgentController(
    IAgentService agentService) : Controller
{
    [HttpPost("chat")]
    public async Task<IActionResult> Chat(
    [FromBody] AgentRequest request,
    CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new
            {
                message = "La pregunta no puede estar vacía."
            });
        }

        var answer = await agentService.AskAsync(
        request.Message,
        cancellationToken);

            return Ok(new AgentResponse
            {
                Answer = answer
            });
    }
}

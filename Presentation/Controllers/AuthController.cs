using Google.Apis.Auth;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Services;
using MantenimientoMaquinasAPI.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MantenimientoMaquinasAPI.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(
    IConfiguration configuration,
    IUsersRepositoryWR usersRepository,
    IJwtTokenService jwtTokenService,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("google-login")]
    [ProducesResponseType(typeof(UserAuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserAuthResponse>> GoogleLogin(
        [FromBody] GoogleLoginRequest request,
        CancellationToken cancellationToken)
    {
        var googleClientId = configuration["Google:ClientId"];
        if (string.IsNullOrWhiteSpace(googleClientId))
        {
            logger.LogError("Google:ClientId is not configured.");
            return Problem("El inicio de sesión con Google no está configurado.", statusCode: 500);
        }

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                request.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleClientId }
                });
        }
        catch (InvalidJwtException)
        {
            return Unauthorized(new { message = "El token de Google no es válido o ha vencido." });
        }

        if (!payload.EmailVerified || string.IsNullOrWhiteSpace(payload.Email))
        {
            return Unauthorized(new { message = "Google no confirmó el correo de esta cuenta." });
        }

        var user = await usersRepository.GetByEmail(payload.Email, cancellationToken);
        if (user is null)
        {
            return Unauthorized(new { message = "Este correo no está registrado en el sistema." });
        }

        if (!string.Equals(user.UserStatus, "ACTIVO", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new { message = "La cuenta de este usuario está inactiva." });
        }

        if (string.IsNullOrWhiteSpace(user.RoleName))
        {
            return Unauthorized(new { message = "El usuario no tiene un rol asignado." });
        }

        try
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(GetTokenLifetimeMinutes());
            return Ok(new UserAuthResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}".Trim(),
                Role = user.RoleName,
                Picture = payload.Picture,
                Token = jwtTokenService.GenerateToken(user),
                ExpiresAt = expiresAt
            });
        }
        catch (InvalidOperationException exception)
        {
            logger.LogError(exception, "JWT authentication is not configured correctly.");
            return Problem("El inicio de sesión no está configurado correctamente.", statusCode: 500);
        }
    }

    private int GetTokenLifetimeMinutes() =>
        int.TryParse(configuration["Jwt:ExpirationMinutes"], out var minutes) && minutes > 0
            ? minutes
            : 60;
}

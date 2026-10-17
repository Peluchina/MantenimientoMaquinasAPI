using System.ComponentModel.DataAnnotations;

namespace MantenimientoMaquinasAPI.Application.Dto;

public sealed class AgentRequest
{
    [Required(ErrorMessage = "La pregunta es obligatoria.")]
    [StringLength(2000, MinimumLength = 2,
    ErrorMessage = "La pregunta debe tener entre 2 y 2000 caracteres.")]
    public string Message { get; set; } = string.Empty;
}

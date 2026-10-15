using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class MachineTemperatureHistoryCreateCommandValidator
    : AbstractValidator<MachineTemperatureHistoryCreateCommand>
{
    public MachineTemperatureHistoryCreateCommandValidator()
    {
        RuleFor(x => x.Params.MachineId).GreaterThan(0).WithMessage("El identificador de máquina debe ser válido.");
        RuleFor(x => x.Request.Temperature).InclusiveBetween(0m, 9999.99m)
            .WithMessage("La temperatura debe estar entre 0 y 9999.99.");
        RuleFor(x => x.Request.UserId).Must(userId => userId is null or > 0)
            .WithMessage("El identificador de usuario debe ser mayor que cero.");
    }
}

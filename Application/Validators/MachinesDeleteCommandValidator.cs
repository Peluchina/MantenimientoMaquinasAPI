using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class MachinesDeleteCommandValidator : AbstractValidator<MachinesDeleteCommand>
{
    public MachinesDeleteCommandValidator() =>
        RuleFor(x => x.Params.MachineId).GreaterThan(0).WithMessage("El identificador debe ser mayor que cero.");
}

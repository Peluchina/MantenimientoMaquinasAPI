using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class UsersDeleteCommandValidator : AbstractValidator<UsersDeleteCommand>
{
    public UsersDeleteCommandValidator() =>
        RuleFor(x => x.Params.UserId).GreaterThan(0).WithMessage("El identificador debe ser mayor que cero.");
}

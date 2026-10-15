using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class UsersUpdateCommandValidator : AbstractValidator<UsersUpdateCommand>
{
    public UsersUpdateCommandValidator()
    {
        RuleFor(x => x.Params.UserId).GreaterThan(0).WithMessage("El identificador debe ser mayor que cero.");
        RuleFor(x => x.Request.RoleId).GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        RuleFor(x => x.Request.Username).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(50)
            .WithMessage("El nombre de usuario es obligatorio y admite hasta 50 caracteres.");
        RuleFor(x => x.Request.FirstName).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(100)
            .WithMessage("El nombre es obligatorio y admite hasta 100 caracteres.");
        RuleFor(x => x.Request.LastName).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(100)
            .WithMessage("El apellido es obligatorio y admite hasta 100 caracteres.");
        RuleFor(x => x.Request.Email).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress().MaximumLength(150)
            .WithMessage("Ingrese un correo válido de hasta 150 caracteres.");
        RuleFor(x => x.Request.UserStatus).Must(status => status is not null &&
            (status.Trim().Equals("ACTIVO", StringComparison.OrdinalIgnoreCase) || status.Trim().Equals("INACTIVO", StringComparison.OrdinalIgnoreCase)))
            .WithMessage("El estado debe ser ACTIVO o INACTIVO.");
    }
}

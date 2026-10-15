using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class UsersCreateCommandValidator : AbstractValidator<UsersCreateCommand>
{
    public UsersCreateCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Los datos del usuario son obligatorios.");
        RuleFor(x => x.Request.RoleId).GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        RuleFor(x => x.Request.Username).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(50)
            .WithMessage("El nombre de usuario es obligatorio y admite hasta 50 caracteres.");
        RuleFor(x => x.Request.FirstName).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(100)
            .WithMessage("El nombre es obligatorio y admite hasta 100 caracteres.");
        RuleFor(x => x.Request.LastName).Cascade(CascadeMode.Stop).Must(value => !string.IsNullOrWhiteSpace(value)).MaximumLength(100)
            .WithMessage("El apellido es obligatorio y admite hasta 100 caracteres.");
        RuleFor(x => x.Request.Email).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress().MaximumLength(150)
            .WithMessage("Ingrese un correo válido de hasta 150 caracteres.");
        RuleFor(x => x.Request.Password).Cascade(CascadeMode.Stop).NotEmpty().MinimumLength(8).MaximumLength(128)
            .WithMessage("La contraseña debe tener entre 8 y 128 caracteres.");
    }
}

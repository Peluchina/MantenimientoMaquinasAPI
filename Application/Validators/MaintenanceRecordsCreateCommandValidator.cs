using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class MaintenanceRecordsCreateCommandValidator : AbstractValidator<MaintenanceRecordsCreateCommand>
{
    public MaintenanceRecordsCreateCommandValidator()
    {
        RuleFor(x => x.Params.MachineId).GreaterThan(0).WithMessage("El identificador de máquina debe ser válido.");
        RuleFor(x => x.Request.UserId).GreaterThan(0).WithMessage("Debe indicar un usuario válido.");
        RuleFor(x => x.Request.MaintenanceDate).GreaterThan(DateTime.MinValue)
            .WithMessage("La fecha del mantenimiento es obligatoria.");
        RuleFor(x => x.Request.MaintenanceType).Cascade(CascadeMode.Stop)
            .Must(value => !string.IsNullOrWhiteSpace(value)).WithMessage("El tipo de mantenimiento es obligatorio.")
            .MaximumLength(50).WithMessage("El tipo de mantenimiento admite hasta 50 caracteres.");
        RuleFor(x => x.Request.Description).Cascade(CascadeMode.Stop)
            .Must(value => !string.IsNullOrWhiteSpace(value)).WithMessage("La descripción es obligatoria.")
            .MaximumLength(500).WithMessage("La descripción admite hasta 500 caracteres.");
        RuleFor(x => x.Request.Cost).Must(cost => cost is null || cost >= 0m && cost <= 9_999_999_999.99m)
            .WithMessage("El costo debe ser mayor o igual que cero y no superar 9,999,999,999.99.");
    }
}

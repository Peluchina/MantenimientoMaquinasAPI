using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class MachinesCreateCommandValidator : AbstractValidator<MachinesCreateCommand>
{
    public MachinesCreateCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Los datos de la máquina son obligatorios.");
        RuleFor(x => x.Request.MachineName).Cascade(CascadeMode.Stop).NotEmpty()
            .WithMessage("El nombre de la máquina es obligatorio.").MaximumLength(100)
            .WithMessage("El nombre no puede superar los 100 caracteres.");
        RuleFor(x => x.Request.Brand).Cascade(CascadeMode.Stop).NotEmpty()
            .WithMessage("La marca de la máquina es obligatoria.").MaximumLength(100)
            .WithMessage("La marca no puede superar los 100 caracteres.");
        RuleFor(x => x.Request.CurrentTemperature).InclusiveBetween(0, Machines.MaximumSupportedTemperature)
            .WithMessage("La temperatura actual debe estar entre 0 y 9999.99.");
        RuleFor(x => x.Request.MaxTemperature).InclusiveBetween(0.01m, Machines.MaximumSupportedTemperature)
            .WithMessage("La temperatura máxima debe ser mayor que 0 y no superar 9999.99.");
        RuleFor(x => x.Request.MachineStatus).Must(status => string.IsNullOrWhiteSpace(status) || Machines.AllowedStatuses.Contains(status.Trim()))
            .WithMessage("El estado debe ser OPERATIVA, MANTENIMIENTO, INACTIVA o FALLA.");
    }
}

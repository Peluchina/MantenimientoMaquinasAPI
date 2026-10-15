using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Validators;

public sealed class MachinesUpdateCommandValidator : AbstractValidator<MachinesUpdateCommand>
{
    public MachinesUpdateCommandValidator()
    {
        RuleFor(x => x.Params.MachineId).GreaterThan(0).WithMessage("El identificador debe ser mayor que cero.");
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
        RuleFor(x => x.Request.MachineStatus).Must(status => status is not null && Machines.AllowedStatuses.Contains(status.Trim()))
            .WithMessage("El estado debe ser OPERATIVA, MANTENIMIENTO, INACTIVA o FALLA.");
    }
}

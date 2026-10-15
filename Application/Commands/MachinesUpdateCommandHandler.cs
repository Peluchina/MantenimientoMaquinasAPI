using FluentValidation;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MachinesUpdateCommandHandler(
    IMachinesRespositoryWR repository,
    IValidator<MachinesUpdateCommand> validator)
{
    public async Task<bool> Update(MachinesUpdateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var machine = new Machines
        {
            MachineId = command.Params.MachineId,
            MachineName = command.Request.MachineName!.Trim(),
            Brand = command.Request.Brand!.Trim(),
            CurrentTemperature = command.Request.CurrentTemperature,
            MaxTemperature = command.Request.MaxTemperature,
            MachineStatus = command.Request.MachineStatus!.Trim().ToUpperInvariant(),
            LastMaintenanceDate = command.Request.LastMaintenanceDate
        };
        return await repository.Update(machine, ct);
    }
}

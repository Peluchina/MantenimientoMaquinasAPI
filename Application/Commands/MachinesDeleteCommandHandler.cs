using FluentValidation;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MachinesDeleteCommandHandler(
    IMachinesRespositoryWR repository,
    IValidator<MachinesDeleteCommand> validator)
{
    public async Task<bool> Delete(MachinesDeleteCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        return await repository.Delete(command.Params.MachineId, ct);
    }
}

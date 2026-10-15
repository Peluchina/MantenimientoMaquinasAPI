using FluentValidation;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Mapping;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MachinesCreateCommandHandler(
    IMachinesRespositoryWR repository,
    IValidator<MachinesCreateCommand> validator)
{
    public async Task<MachinesCreateResponse> Create(MachinesCreateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var id = await repository.Insert(MachinesCreateCommandMapping.ToEntity(command), ct);
        return new MachinesCreateResponse { MachineId = id };
    }
}

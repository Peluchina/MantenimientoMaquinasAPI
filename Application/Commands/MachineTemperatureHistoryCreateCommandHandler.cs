using FluentValidation;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Mapping;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MachineTemperatureHistoryCreateCommandHandler(
    IMachineTemperatureHistoryRespositoryWR repository,
    IValidator<MachineTemperatureHistoryCreateCommand> validator)
{
    public async Task<MachineTemperatureHistoryCreateResponse> Create(
        MachineTemperatureHistoryCreateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var id = await repository.Insert(MachineTemperatureHistoryCreateCommandMapping.ToEntity(command), ct);
        return new MachineTemperatureHistoryCreateResponse { TemperatureHistoryId = id };
    }
}

using FluentValidation;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Mapping;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class MaintenanceRecordsCreateCommandHandler(
    IMaintenanceRecordsRespositoryWR repository,
    IValidator<MaintenanceRecordsCreateCommand> validator)
{
    public async Task<MaintenanceRecordsCreateResponse> Create(
        MaintenanceRecordsCreateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var id = await repository.Insert(MaintenanceRecordsCreateCommandMapping.ToEntity(command), ct);
        return new MaintenanceRecordsCreateResponse { MaintenanceId = id };
    }
}

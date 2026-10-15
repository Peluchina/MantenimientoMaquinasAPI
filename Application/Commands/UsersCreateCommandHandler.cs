using FluentValidation;
using MantenimientoMaquinasAPI.Application.Dto;
using MantenimientoMaquinasAPI.Application.Mapping;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class UsersCreateCommandHandler(
    IUsersRepositoryWR repository,
    IValidator<UsersCreateCommand> validator)
{
    public async Task<UsersCreateResponse> Create(UsersCreateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var userId = await repository.Insert(UsersCreateCommandMapping.ToEntity(command), ct);
        return new UsersCreateResponse { UserId = userId };
    }
}

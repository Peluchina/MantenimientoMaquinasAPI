using FluentValidation;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class UsersDeleteCommandHandler(
    IUsersRepositoryWR repository,
    IValidator<UsersDeleteCommand> validator)
{
    public async Task<bool> Delete(UsersDeleteCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        return await repository.Deactivate(command.Params.UserId, ct);
    }
}

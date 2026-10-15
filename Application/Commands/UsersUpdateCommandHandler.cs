using FluentValidation;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Commands;

public sealed class UsersUpdateCommandHandler(
    IUsersRepositoryWR repository,
    IValidator<UsersUpdateCommand> validator)
{
    public async Task<bool> Update(UsersUpdateCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var request = command.Request;
        var user = new Users
        {
            UserId = command.Params.UserId,
            RoleId = request.RoleId,
            Username = request.Username.Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            UserStatus = request.UserStatus.Trim().ToUpperInvariant()
        };
        return await repository.Update(user, ct);
    }
}

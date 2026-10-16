using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;

namespace MantenimientoMaquinasAPI.Infrastructure.Repository;

public sealed class UsersRepository(IConnectionSQL connectionFactory) : IUsersRepositoryWR
{
    public async Task<IEnumerable<Users>> GetAll(CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        var users = await connection.QueryAsync<Users>(new CommandDefinition(
            "dbo.Users_GetAll", commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
        return users.AsList();
    }

    public async Task<Users?> GetById(int id, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<Users>(new CommandDefinition(
            "dbo.Users_GetById", new { UserId = id }, commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken));
    }

    public async Task<Users?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<Users>(new CommandDefinition(
            """
            SELECT U.UserId, U.RoleId, U.Username, U.FirstName, U.LastName, U.Email,
                   U.UserStatus, R.RoleName
            FROM dbo.Users AS U
            INNER JOIN dbo.Roles AS R ON R.RoleId = U.RoleId
            WHERE U.Email = @Email;
            """,
            new { Email = email }, cancellationToken: cancellationToken));
    }

    public async Task<int> Insert(Users user, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        return await connection.QuerySingleAsync<int>(new CommandDefinition("dbo.Users_Create",
            new { user.RoleId, user.Username, user.FirstName, user.LastName, user.Email, user.PasswordHash },
            commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
    }

    public async Task<bool> Update(Users user, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        try
        {
            await connection.ExecuteAsync(new CommandDefinition("dbo.Users_Update",
                new { user.UserId, user.RoleId, user.Username, user.FirstName, user.LastName, user.Email, user.UserStatus },
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            return true;
        }
        catch (SqlException exception) when (exception.Number == 50104) { return false; }
    }

    public async Task<bool> Deactivate(int id, CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.GetConnection();
        try
        {
            await connection.ExecuteAsync(new CommandDefinition("dbo.Users_Delete", new { UserId = id },
                commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
            return true;
        }
        catch (SqlException exception) when (exception.Number == 50105) { return false; }
    }
}

using System.Security.Cryptography;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Domain.Agregates;

namespace MantenimientoMaquinasAPI.Application.Mapping;

public static class UsersCreateCommandMapping
{
    public static Users ToEntity(UsersCreateCommand command)
    {
        var request = command.Request;
        return new Users
        {
            RoleId = request.RoleId,
            Username = request.Username.Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            PasswordHash = HashPassword(request.Password)
        };
    }

    private static string HashPassword(string password)
    {
        const int iterations = 210_000;
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
        return $"PBKDF2-SHA256${iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }
}

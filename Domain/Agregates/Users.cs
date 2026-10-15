namespace MantenimientoMaquinasAPI.Domain.Agregates
{
    public class Users
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? UserStatus { get; set; }
        public string? RoleName { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

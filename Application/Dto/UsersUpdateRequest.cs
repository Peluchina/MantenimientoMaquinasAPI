namespace MantenimientoMaquinasAPI.Application.Dto
{
    public class UsersUpdateRequest
    {
        public int RoleId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserStatus { get; set; } = "ACTIVO";
    }
}

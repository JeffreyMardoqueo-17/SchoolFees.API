namespace SchoolFees.API.DTOs.Roles
{
    public class RoleReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Será null si es rol global
        public string? InstitucionName { get; set; }
    }
}

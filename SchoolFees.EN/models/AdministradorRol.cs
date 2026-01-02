namespace SchoolFees.EN.models
{
    public class AdministradorRol
    {
        public int IdAdministrador { get; set; }
        public Administrador Administrador { get; set; } = null!;

        public int IdRol { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}

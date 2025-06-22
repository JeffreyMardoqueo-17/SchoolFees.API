namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Representa un permiso específico dentro del sistema (crear usuario, ver pagos, etc.)
    /// </summary>
    public class Permission
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Code { get; private set; } // Identificador único como "PAYMENT_VIEW", "USER_CREATE"
        public string Description { get; private set; }

        private Permission() { }

        public Permission(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código del permiso no puede estar vacío.");

            Code = code;
            Description = description;
        }
    }
}

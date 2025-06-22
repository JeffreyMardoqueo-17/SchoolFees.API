using System;

namespace SchoolFees.Domain.Entities
{
     /// <summary>
    /// Representa a un usuario del sistema (estudiante, encargado, docente, administrador, etc.).
    /// Cada usuario está vinculado a una institución y a un rol, el cual define sus permisos.
    /// </summary>
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid(); // ID único del usuario

        public string Name { get; private set; } = string.Empty; // Nombre del usuario
        public string LastName { get; private set; } = string.Empty; // Apellido
        public string Email { get; private set; } = string.Empty; // Correo electrónico
        public string PhoneNumber { get; private set; } = string.Empty; // Teléfono
        public string Password { get; private set; } = string.Empty; // Contraseña encriptada
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow; // Fecha de creación
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
        public Guid InstitutionId { get; private set; }
        public Institution Institution { get; private set; } = null!;

        public bool IsActive { get; private set; } = true;

        // Constructor requerido por EF
        private User() { }

        // Constructor principal (sin asignar rol directamente)
        public User(
            string name,
            string lastName,
            string email,
            string phoneNumber,
            string password,
            Guid institutionId)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            InstitutionId = institutionId;
        }

        /// <summary>
        /// Asigna un rol al usuario (por ejemplo: "Student", "Admin", etc.)
        /// </summary>
        public void AssignRole(Role role)
        {
            if (role == null)
                throw new ArgumentException("El rol no puede ser nulo.", nameof(role));
            Role = role;
            RoleId = role.Id;
        }

        /// <summary>
        /// Desactiva al usuario
        /// </summary>
        public void Deactivate() => IsActive = false;

        /// <summary>
        /// Activa al usuario
        /// </summary>
        public void Activate() => IsActive = true;
    }
}

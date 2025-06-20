using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Identificador único del usuario
        public string Name { get; set; } = string.Empty; // Nombre del usuario
        public string LastName { get; set; } = string.Empty; // Apellido del usuario
        public string Email { get; set; } = string.Empty; // Correo electrónico del usuario
        public string PhoneNumber { get; set; } = string.Empty; // Número de teléfono del usuario
        public string Password { get; set; } = string.Empty; // Contraseña del usuario
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Fecha de creación del usuario

        //relaciones
        public UserRole Role { get; set; } = UserRole.Student; // Rol del usuario (por defecto es estudiante)
        public Guid InstitutionId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public Institution Institution { get; set; } = null!; // Institución a la que pertenece el usuario

        public User(string name, string lastName, string email, string phoneNumber, string password, UserRole role, Guid institutionId)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
            InstitutionId = institutionId;
        }
        ///
        /// Metodo para desactivar al usuario
        public void Deactivate() => IsActive = false;
    }
}
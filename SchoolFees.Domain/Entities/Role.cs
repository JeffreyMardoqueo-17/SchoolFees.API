using System;
using System.Collections.Generic;

namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Representa un rol dentro del sistema (Admin, Student, Guardian, Teacher, etc.)
    /// Cada usuario puede tener un rol asignado, y en el futuro este rol puede tener permisos asociados.
    /// </summary>
    public class Role
    {
        public Guid Id { get; private set; } = Guid.NewGuid(); // Identificador único del rol
        public string Name { get; private set; } // Nombre del rol (por ejemplo: "Admin", "Student", "Teacher")
        public string Description { get; private set; } // Descripción opcional del rol

        // (Opcional) Lista de permisos si deseas implementar un sistema RBAC en el futuro
        private readonly List<Permission> _permissions = new();
        public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

        // Constructor
        private Role() { }

        // Constructor principal
        public Role(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del rol no puede estar vacío.");

            Name = name;
            Description = description;
        }

        // Método para agregar un permiso (opcional si usas control de permisos por rol)
        public void AddPermission(Permission permission)
        {
            if (_permissions.Exists(p => p.Code == permission.Code))
                return;
            _permissions.Add(permission);
        }
        // Método para eliminar un permiso
        public void RemovePermission(string code)
        {
            var permission = _permissions.Find(p => p.Code == code);
            if (permission != null)
                _permissions.Remove(permission);
        }
    }
}

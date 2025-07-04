using System;

namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Entidad intermedia para representar la relación muchos-a-muchos entre Role y Permission.
    /// </summary>
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;

        public Guid PermissionId { get; private set; }
        public Permission Permission { get; private set; } = null!;

        private RolePermission() { }

        public RolePermission(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}

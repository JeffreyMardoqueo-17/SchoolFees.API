using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.Repositories
{
    public interface IChangePasswordRepository
    {

        /// <summary>
        /// Acceso a datos para cambio de contraseña.
        /// </summary>
        public interface IChangePasswordRepository
        {
            /// <summary>Verifica si la contraseña actual es correcta. y va a debolver un bool(true or false)</summary>
            Task<bool> VerifyCurrentPasswordAsync(Guid userId, string currentPassword);
            /// <summary>Persiste el nuevo hash de contraseña.</summary>
            Task<bool> SetNewPasswordHashAsync(Guid userId, string newPasswordHash);
        }
    }
}
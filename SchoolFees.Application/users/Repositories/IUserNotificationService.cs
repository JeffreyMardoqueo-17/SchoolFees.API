using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.Repositories
{
    public interface IUserNotificationService
    {/// <summary>
     /// Envia una notificación al usuario cuando se crea su cuenta.
     /// /// </summary>
        Task SendPasswordChangedAsync(Guid userId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IAdministradorRolRepository
    {
        Task AddAsync(AdministradorRol administradorRol);
        Task AddRangeAsync(IEnumerable<AdministradorRol> administradorRoles);
        Task <AdministradorRol?> GetByIdAsync(int IdAdministrador, int IdRol);
    }
}
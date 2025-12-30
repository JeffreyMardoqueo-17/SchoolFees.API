using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IGrupoRepository
    {
        Task<IEnumerable<Grupo>> GetAllGrupo();
        Task<Grupo?> GetByIdGrupoAsync(int id);
    }
}
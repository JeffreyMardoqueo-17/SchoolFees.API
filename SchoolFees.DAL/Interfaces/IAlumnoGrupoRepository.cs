using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.Models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IAlumnoGrupoRepository
    {
        Task<IEnumerable<AlumnoGrupo>> GetAllAlumnoGrupo();
        Task<AlumnoGrupo?> GetByIdAlumnoGrupoAsync(int idAlumno, int idGrupo);
        Task CreateAsync(AlumnoGrupo entity);
    }
}
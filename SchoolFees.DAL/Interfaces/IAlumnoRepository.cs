using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IAlumnoRepository : ICrudRepository<Alumno>
    {
        // Task<Alumno> GetByCodigoAlumnoAsync(string codigoAlumno);
        Task<IEnumerable<Alumno?>> GetInactivosAsync();

    }
}
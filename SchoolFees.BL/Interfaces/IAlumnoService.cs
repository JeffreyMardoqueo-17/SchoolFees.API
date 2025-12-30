using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Interfaces
{
    public interface IAlumnoService : ICrudService<Alumno>
    {
        Task<IEnumerable<Alumno?>> GetInactivosAsync();
               Task<Alumno> PostAsync(Alumno entity, int idGrado);
    }
}
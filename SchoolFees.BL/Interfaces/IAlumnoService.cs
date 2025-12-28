using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.BL.Interfaces
{
    public interface IAlumnoService  : ICrudRepository
    {
            Task<IEnumerable<Alumno?>> GetInactivosAsync();
    }
}
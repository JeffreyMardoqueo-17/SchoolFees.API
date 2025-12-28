using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Interfaces
{
    public interface ICrudService
    {
        Task<Alumno> CrearAsync(Alumno alumno);
        Task<IEnumerable<Alumno>> ObtenerActivosAsync();
        Task<Alumno?> ObtenerPorIdAsync(int id);
        Task ActualizarAsync(Alumno alumno);
        Task EliminarAsync(int id);
    }
}
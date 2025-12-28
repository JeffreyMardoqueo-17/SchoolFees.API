using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Interfaces
{
    public interface IGradoRepository
    {
        Task <IEnumerable<Grado>> GetAllGrado();
        Task<Grado> GetByIdGradoAsync(int id); //y para saber si existe
        
    }
}
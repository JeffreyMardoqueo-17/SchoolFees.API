using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Repositories
{
    public class GradoRespository : IGradoRepository
    {
        private readonly SchoolFeesDbContext _context;
        public GradoRespository(SchoolFeesDbContext context)
        {
            _context = context;
        }


        ///metodoo para traer todos los grados 
        public async Task<IEnumerable<Grado>> GetAllGrado()
        {
            return await _context.Grado
                .AsNoTracking()
                .Where(g => g.Estado == true)
                .ToListAsync();
        }
        //metodo para obtener por iD
        public async Task<Grado?> GetByIdGradoAsync(int id)
        {
            return await _context.Grado
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id && g.Estado == true);
        }
    }
}
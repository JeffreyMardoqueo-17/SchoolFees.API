using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.Models;
using SchoolFees.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace SchoolFees.DAL.Repositories
{
    public class AlumnoGrupoRepository : IAlumnoGrupoRepository
    {
        private readonly SchoolFeesDbContext _context;
        public AlumnoGrupoRepository(SchoolFeesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AlumnoGrupo>> GetAllAlumnoGrupo()
        {
            return await _context.AlumnoGrupo.ToListAsync();
        }
        public async Task<AlumnoGrupo?> GetByIdAlumnoGrupoAsync(int idAlumno, int idGrupo)
        {
            return await _context.AlumnoGrupo
                .FirstOrDefaultAsync(ag => ag.IdAlumno == idAlumno && ag.IdGrupo == idGrupo);
        }
        public async Task CreateAsync(AlumnoGrupo entity)
        {
            _context.AlumnoGrupo.Add(entity);
            await _context.SaveChangesAsync();
        }

    }
}
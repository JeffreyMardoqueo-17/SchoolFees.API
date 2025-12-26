using Microsoft.EntityFrameworkCore;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;
// using SchoolFees.EN.Models;

namespace SchoolFees.DAL.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly SchoolFeesDbContext _context;

        public AlumnoRepository(SchoolFeesDbContext context)
        {
            _context = context;
        }

        public async Task<Alumno> CreateAsync(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            return await _context.Alumnos
                .AsNoTracking()
                .Where(a => a.Estado)
                .ToListAsync();
        }

        public async Task<Alumno?> GetByIdAsync(int id)
        {
            return await _context.Alumnos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.Estado);
        }

        public async Task<IEnumerable<Alumno?>> GetInactivosAsync()
        {
            return await _context.Alumnos
                .AsNoTracking()
                .Where(a => !a.Estado)
                .ToListAsync();
        }

        public async Task UpdateAsync(Alumno alumno)
        {
            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null) return;

            alumno.Estado = false; // Soft delete
            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Repositories
{
    public class AdministradorRolRepository : IAdministradorRolRepository
    {
        private readonly SchoolFeesDbContext _context;

        public AdministradorRolRepository(SchoolFeesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AdministradorRol administradorRol)
        {
            _context.AdministradorRol.Add(administradorRol);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<AdministradorRol> administradorRoles)
        {
            _context.AdministradorRol.AddRange(administradorRoles);
            await _context.SaveChangesAsync();
        }
        public async Task<AdministradorRol?> GetByIdAsync(int IdAdministrador, int IdRol)
        {
            return await _context.AdministradorRol
                .FirstOrDefaultAsync(ar => ar.IdAdministrador == IdAdministrador && ar.IdRol == IdRol);

        }

        // public async Task<AlumnoGrupo?> GetByIdAlumnoGrupoAsync(int idAlumno, int idGrupo)
        // {
        //     return await _context.AlumnoGrupo
        //         .FirstOrDefaultAsync(ag => ag.IdAlumno == idAlumno && ag.IdGrupo == idGrupo);
        // }
    }
}

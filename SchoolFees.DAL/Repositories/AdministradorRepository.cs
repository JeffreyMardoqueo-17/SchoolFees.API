using Microsoft.EntityFrameworkCore;
using SchoolFees.DAL.Context;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;
using SchoolFees.EN.Models;

namespace SchoolFees.DAL.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly SchoolFeesDbContext _context;

        public AdministradorRepository(SchoolFeesDbContext context)
        {
            _context = context;
        }

        // ---------------- CRUD ----------------

        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            return await _context.Administrador
                .AsNoTracking()
                .Where(a => a.Estado)
                .ToListAsync();
        }

        public async Task<Administrador?> GetByIdAsync(int id)
        {
            return await _context.Administrador
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Administrador?> GetByCorreoAsync(string correo)
        {
            return await _context.Administrador
                .FirstOrDefaultAsync(a => a.Correo == correo);
        }

        public async Task CreateAsync(Administrador administrador)
        {
            _context.Administrador.Add(administrador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Administrador administrador)
        {
            _context.Administrador.Update(administrador);
            await _context.SaveChangesAsync();
        }

        // ---------------- Estados ----------------

        public async Task DesactivarAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.Estado = false;
            await _context.SaveChangesAsync();
        }

        public async Task ActivarAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.Estado = true;
            await _context.SaveChangesAsync();
        }

        // ---------------- Seguridad ----------------

        public async Task UpdatePasswordAsync(int id, string passwordHash, string passwordSalt)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoginInfoAsync(int id, DateTime ultimoLogin, string ip)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.UltimoLogin = ultimoLogin;
            admin.UltimaIP = ip;
            await _context.SaveChangesAsync();
        }

        public async Task IncrementarIntentosFallidosAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.IntentosFallidos++;
            await _context.SaveChangesAsync();
        }

        public async Task ResetIntentosFallidosAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.IntentosFallidos = 0;
            await _context.SaveChangesAsync();
        }

        public async Task BloquearAsync(int id, DateTime hasta)
        {
            var admin = await GetByIdAsync(id);
            if (admin == null) return;

            admin.BloqueadoHasta = hasta;
            await _context.SaveChangesAsync();
        }

        // ---------------- Consultas de negocio ----------------

        public async Task<int> ContarAdministradoresActivosAsync()
        {
            return await _context.Administrador
                .CountAsync(a => a.Estado == true);
        }
    }
}

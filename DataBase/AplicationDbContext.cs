using Microsoft.EntityFrameworkCore;
using SchoolFees.API.Models;
namespace SchoolFees.API.DataBase
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }

        //agrego los db set
        public DbSet<TipoPago> TipoPago { get; set; } 
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Turno> Tuno { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TipoInstitucion> TipoInstitucion { get; set; }
    }
}

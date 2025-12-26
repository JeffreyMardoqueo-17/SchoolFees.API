using Microsoft.EntityFrameworkCore;
using SchoolFees.EN.models;

namespace SchoolFees.DAL.Context
{
    public class SchoolFeesDbContext : DbContext
    {
        public SchoolFeesDbContext(DbContextOptions<SchoolFeesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones por Fluent API (si no usas Data Annotations)
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolFeesDbContext).Assembly);
        }
    }
}

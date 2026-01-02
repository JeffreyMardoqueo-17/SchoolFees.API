using Microsoft.EntityFrameworkCore;
using SchoolFees.EN.models;
using SchoolFees.EN.Models;

namespace SchoolFees.DAL.Context
{
    public class SchoolFeesDbContext : DbContext
    {
        public SchoolFeesDbContext(DbContextOptions<SchoolFeesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumno { get; set; } = null!;
        public DbSet<Grado> Grado { get; set; } = null!;
        public DbSet<Grupo> Grupo { get; set; } = null!;
        public DbSet<AlumnoGrupo> AlumnoGrupo { get; set; } = null!;

        //adminsitracion
        public DbSet<Administrador> Administrador { get; set; } = null!;


        /// <summary>
        /// administracion
        /// </summary>
        public DbSet<Rol> Rol { get; set; } = null!;
        

        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AlumnoGrupo>()
                .HasKey(ag => new { ag.IdAlumno, ag.IdGrupo });
            modelBuilder.Entity<AlumnoGrupo>(entity =>
            {
                entity.HasKey(e => new { e.IdAlumno, e.IdGrupo });

                entity.Property(e => e.FechaAsignacion)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne<Alumno>()
                    .WithMany()
                    .HasForeignKey(e => e.IdAlumno)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Grupo>()
                    .WithMany()
                    .HasForeignKey(e => e.IdGrupo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}

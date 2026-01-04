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
        public DbSet<AdministradorRol> AdministradorRol { get; set; } = null!;

        /// <summary>
        /// administracion
        /// </summary>
        public DbSet<Rol> Rol { get; set; } = null!;


        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AlumnoGrupo (ya lo tenías bien)
            modelBuilder.Entity<AlumnoGrupo>()
                .HasKey(ag => new { ag.IdAlumno, ag.IdGrupo });

            // 🔥 AdministradorRol (ESTO ARREGLA TODO)
            modelBuilder.Entity<AdministradorRol>()
                .HasKey(ar => new { ar.IdAdministrador, ar.IdRol });

            modelBuilder.Entity<AdministradorRol>()
                .HasOne(ar => ar.Administrador)
                .WithMany(a => a.Roles)
                .HasForeignKey(ar => ar.IdAdministrador);

            modelBuilder.Entity<AdministradorRol>()
                .HasOne(ar => ar.Rol)
                .WithMany()
                .HasForeignKey(ar => ar.IdRol);
        }

    }
}

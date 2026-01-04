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

            // =========================
            // ALUMNO - GRUPO (N:N)
            // =========================
            modelBuilder.Entity<AlumnoGrupo>()
      .HasKey(ag => new { ag.IdAlumno, ag.IdGrupo });

            modelBuilder.Entity<AlumnoGrupo>()
                .HasOne(ag => ag.Alumno)
                .WithMany(a => a.Grupos)
                .HasForeignKey(ag => ag.IdAlumno)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlumnoGrupo>()
                .HasOne(ag => ag.Grupo)
                .WithMany(g => g.Grupos)
                .HasForeignKey(ag => ag.IdGrupo)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // ADMINISTRADOR - ROL (N:N)
            // =========================
            modelBuilder.Entity<AdministradorRol>()
                .HasKey(ar => new { ar.IdAdministrador, ar.IdRol });

            modelBuilder.Entity<AdministradorRol>()
                .HasOne(ar => ar.Administrador)
                .WithMany(a => a.Roles)
                .HasForeignKey(ar => ar.IdAdministrador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdministradorRol>()
                .HasOne(ar => ar.Rol)
                .WithMany(r => r.Administradores)
                .HasForeignKey(ar => ar.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // ADMINISTRADOR
            // =========================
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasIndex(a => a.Correo)
                      .IsUnique();

                entity.Property(a => a.Correo)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(a => a.Nombres)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.Apellidos)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // =========================
            // ROL
            // =========================
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(r => r.Nombre)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(r => r.Nombre)
                      .IsUnique();
            });
        }


    }
}

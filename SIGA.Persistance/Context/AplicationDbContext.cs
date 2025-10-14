using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIGA.Domain.Entities;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Persistance.Context
{
    public class AplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<CursoAcademico> CursosAcademicos { get; set; }
        public DbSet<ClaseProgramada> ClasesProgramadas { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // relacion de estudiantes y profesores con ApplicationUserId"
            builder.Entity<Estudiante>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Estudiante>(e => e.ApplicationUserId)
                .IsRequired();

            builder.Entity<Profesor>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Profesor>(p => p.ApplicationUserId)
                .IsRequired();

          
            // Para que en caso de que borremos un grado noborre los CursosAcademicos.
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}

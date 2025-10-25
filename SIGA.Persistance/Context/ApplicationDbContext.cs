using Microsoft.EntityFrameworkCore;
using SIGA.Domain.Entities;
using SIGA.Identity.Shared.Entities;

namespace SIGA.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Estudiantes> Estudiantes { get; set; }
        public DbSet<Maestros> Maestros { get; set; }
        /*public DbSet<Grado> Grados { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<CursoAcademico> CursosAcademicos { get; set; }
        public DbSet<ClaseProgramada> ClasesProgramadas { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }*/

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // relacion de estudiantes y profesores con ApplicationUserId"
            builder.Entity<Estudiantes>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Estudiantes>(e => e.UserId)
                .IsRequired();

            builder.Entity<Maestros>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Maestros>(p => p.UserId)
                .IsRequired();


            // Para que en caso de que borremos un grado noborre los CursosAcademicos.
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

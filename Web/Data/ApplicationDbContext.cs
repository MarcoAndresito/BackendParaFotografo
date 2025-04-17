using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<RegistroUsuario> RegistroUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(producto =>
            {
                producto
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

                producto
                .HasKey((p) => p.Id);

                producto
                .Property((p) => p.Nombre)
                .IsRequired()
                .HasMaxLength(50);

                producto
                .Property((p) => p.Precio)
                .IsRequired()
                .HasPrecision(9, 2);
            });

            modelBuilder.Entity<RegistroUsuario>(registroUsuario =>
            {
                registroUsuario
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

                registroUsuario
                .HasKey((ru) => ru.Id);

                registroUsuario
                .Property((ru) => ru.Nombre)
                .IsRequired()
                .HasMaxLength(50);

                registroUsuario
                .Property((ru) => ru.Correo)
                .IsRequired()
                .HasMaxLength(100);

                registroUsuario
                .Property((ru) => ru.Contraseña)
                .IsRequired()
                .HasMaxLength(100);
            });
            modelBuilder.Entity<Comentario>(comentario =>
            {
                comentario
                    .Property(c => c.Id)
                    .ValueGeneratedOnAdd();

                comentario
                    .HasKey(c => c.Id);

                comentario
                    .Property(c => c.Contenido)
                    .IsRequired()
                    .HasMaxLength(500);

                comentario
                    .Property(c => c.FechaCreacion)
                    .IsRequired();

                // UsuarioId puede ser nulo si el comentario es anónimo
                comentario
                    .Property(c => c.UsuarioId)
                    .IsUnicode(false)
                    .HasMaxLength(450);

                comentario
                       .HasIndex(c => c.UsuarioId);

                comentario
                    .HasIndex(c => c.FechaCreacion);
            });
        }
    }
}

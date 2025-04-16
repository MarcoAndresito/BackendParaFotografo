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
        public DbSet<RegistroUsuario> RegistroUsuarios { get; set; }
        public DbSet<ExportarAlbum> Exportaciones { get; set; }

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

            modelBuilder.Entity<ExportarAlbum>(exportar =>
            {
                exportar.HasKey(e => e.Id);
                exportar.Property(e => e.NombreArchivo).IsRequired().HasMaxLength(100);
                exportar.Property(e => e.Formato).IsRequired().HasMaxLength(10);
                exportar.Property(e => e.EnlaceDescarga).HasMaxLength(255);
            });

        }
    }
}

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
        public DbSet<Parametro> Parametros { get; set; }


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
            modelBuilder.Entity<Parametro>(param =>
            {
                param.HasKey(p => p.Id);

                param.Property(p => p.Id)
                     .ValueGeneratedOnAdd();

                param.Property(p => p.Servicio)
                     .IsRequired()
                     .HasMaxLength(100);

                param.Property(p => p.Clave)
                     .IsRequired()
                     .HasMaxLength(100);

                param.Property(p => p.Valor)
                     .IsRequired()
                     .HasMaxLength(200);

                param.Property(p => p.Descripcion)
                     .HasMaxLength(300);

                param.Property(p => p.Activo)
                     .IsRequired();
            });
        }
    }
}

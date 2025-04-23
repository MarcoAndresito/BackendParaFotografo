using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;
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
        public DbSet<RegistroUsuario> Usuarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Album> Albumes { get; set; }
        public DbSet<ExportarAlbum> Exportaciones { get; set; }
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

            modelBuilder.Entity<Foto>(foto =>
            {
                foto
                    .Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                // Define Id como la clave primaria de la tabla Foto.
                foto
                    .HasKey((f) => f.Id);

                foto
                    .Property(f => f.AlbumId)
                    .IsRequired();

                foto
                    .HasOne(f => f.Album)
                    .WithMany(a => a.Fotos)
                    .HasForeignKey(f => f.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade); 

                foto
                    .Property(f => f.NombreArchivo)
                    .IsRequired()
                    .HasMaxLength(255);

                foto
                    .Property(f => f.Url)
                    .IsRequired()
                    .HasMaxLength(2048);

                foto
                    .Property(f => f.Formato)
                    .HasMaxLength(10);

                foto
                    .Property(f => f.PesoKB)
                    .IsRequired();

                foto
                    .Property(f => f.AnchoPx)
                    .IsRequired();

                foto
                    .Property(f => f.AltoPx)
                    .IsRequired();

                foto
                    .Property(f => f.PublicId)
                    .HasMaxLength(255);

                foto
                    .Property(f => f.FechaSubida)
                    .IsRequired();
            });

            modelBuilder.Entity<Album>(album =>
            {
                album
                    .Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                album
                    .HasKey((a) => a.Id);

                album
                    .Property(a => a.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                album
                    .Property(a => a.Descripcion)
                    .HasMaxLength(500);

                album
                    .Property(a => a.UsuarioId)
                    .IsRequired()
                    .HasMaxLength(450);

                album
                    .Property(a => a.FechaCreacion)
                    .IsRequired();

                album
                    .HasMany(a => a.Fotos)
                    .WithOne(f => f.Album)
                    .HasForeignKey(f => f.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade); 
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

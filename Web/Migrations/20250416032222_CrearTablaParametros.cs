using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaParametros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Servicio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");



            migrationBuilder.Sql(@"
                 INSERT INTO Parametros (Servicio, Clave, Valor, Descripcion, Activo)
                    VALUES 
                ('Fotografia', 'resolucion_maxima', '6000x4000', 'Resolución máxima permitida para fotos', 1),
                ('Fotografia', 'formato_default', 'JPEG', 'Formato predeterminado de entrega de fotos', 1),
                ('Galeria', 'max_fotos_por_album', '50', 'Número máximo de fotos por álbum', 1),
                ('Galeria', 'orden_predeterminado', 'fecha_desc', 'Orden predeterminado para mostrar fotos', 1),
                ('Sistema', 'idioma', 'es-BO', 'Idioma predeterminado del sistema', 1),
                ('Sistema', 'zona_horaria', 'America/La_Paz', 'Zona horaria local', 1);
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "RegistroUsuarios");
        }
    }
}

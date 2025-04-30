using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cambiandoTablaFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltoPx",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "AnchoPx",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "Formato",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "NombreArchivo",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "PesoKB",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Fotos");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Fotos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Fotos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "imageBytes",
                table: "Fotos",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "imageBytes",
                table: "Fotos");

            migrationBuilder.AddColumn<int>(
                name: "AltoPx",
                table: "Fotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnchoPx",
                table: "Fotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Formato",
                table: "Fotos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreArchivo",
                table: "Fotos",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PesoKB",
                table: "Fotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Fotos",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Fotos",
                type: "varchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

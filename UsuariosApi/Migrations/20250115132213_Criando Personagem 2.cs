using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UsuariosApi.Migrations
{
    /// <inheritdoc />
    public partial class CriandoPersonagem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Personagens",
                table: "Personagens");

            migrationBuilder.RenameTable(
                name: "Personagens",
                newName: "Personagem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Personagem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personagem",
                table: "Personagem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Personagem",
                table: "Personagem");

            migrationBuilder.RenameTable(
                name: "Personagem",
                newName: "Personagens");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Personagens",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personagens",
                table: "Personagens",
                column: "Id");
        }
    }
}

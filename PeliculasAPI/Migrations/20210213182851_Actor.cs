using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasAPI.Migrations
{
    public partial class Actor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Actors",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Actors",
                newName: "Birthday");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Actors",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Actors",
                newName: "FechaNacimiento");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoCantidadVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Ventas");
        }
    }
}

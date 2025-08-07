using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazPFMCFIC.Migrations
{
    /// <inheritdoc />
    public partial class ajusteModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}

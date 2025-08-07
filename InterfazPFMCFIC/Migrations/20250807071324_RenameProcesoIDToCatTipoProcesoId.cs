using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazPFMCFIC.Migrations
{
    /// <inheritdoc />
    public partial class RenameProcesoIDToCatTipoProcesoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcesoID",
                table: "INTERFAZ_PFM_CFIC_ARCHIVO",
                newName: "CatTipoProcesoId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CatTipoProcesoId",
                table: "INTERFAZ_PFM_CFIC_ARCHIVO",
                newName: "ProcesoID"
            );
        }
    }
}

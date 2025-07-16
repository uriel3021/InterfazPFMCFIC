using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterfazPFMCFIC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_ARCHIVO",
                columns: table => new
                {
                    ArchivoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcesoID = table.Column<int>(type: "int", nullable: true),
                    RegistroID = table.Column<int>(type: "int", nullable: true),
                    Ruta = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    NombreArchivo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PesoArchivoKB = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__3D24276A7FD7C9C3", x => x.ArchivoID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CatCodigoRetorno",
                columns: table => new
                {
                    CatCodigoRetornoID = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFAZ_PFM_CFIC_CatCodigoRetorno", x => x.CatCodigoRetornoID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CatEstatusSolicitud",
                columns: table => new
                {
                    CatEstatusSolicitudID = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFAZ_PFM_CFIC_CatEstatusSolicitud", x => x.CatEstatusSolicitudID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CatTipoConfirmacion",
                columns: table => new
                {
                    CatTipoConfirmacionID = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFAZ_PFM_CFIC_CatTipoConfirmacion", x => x.CatTipoConfirmacionID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CatTipoProducto",
                columns: table => new
                {
                    CatTipoProductoID = table.Column<int>(type: "int", nullable: false),
                    DescripcionProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PFMCFIC = table.Column<bool>(type: "bit", nullable: false),
                    Inactivo = table.Column<bool>(type: "bit", nullable: false),
                    Personas = table.Column<bool>(type: "bit", nullable: true),
                    Vehículos = table.Column<bool>(type: "bit", nullable: true),
                    ArmasyExplosivos = table.Column<bool>(type: "bit", nullable: true),
                    MaterialApocrifo = table.Column<bool>(type: "bit", nullable: true),
                    BienesInmuebles = table.Column<bool>(type: "bit", nullable: true),
                    Numeral = table.Column<bool>(type: "bit", nullable: true),
                    Drogas = table.Column<bool>(type: "bit", nullable: true),
                    EquipoDeComunicacion = table.Column<bool>(type: "bit", nullable: true),
                    EquipoTactico = table.Column<bool>(type: "bit", nullable: true),
                    FaunaProtegida = table.Column<bool>(type: "bit", nullable: true),
                    Ubicacion = table.Column<bool>(type: "bit", nullable: true),
                    Otros = table.Column<bool>(type: "bit", nullable: true),
                    Archivo = table.Column<bool>(type: "bit", nullable: true),
                    PersonasMorales = table.Column<bool>(type: "bit", nullable: true),
                    Alias = table.Column<bool>(type: "bit", nullable: true),
                    OrganizacionesDelictivas = table.Column<bool>(type: "bit", nullable: true),
                    Eventos = table.Column<bool>(type: "bit", nullable: true),
                    InternetuOtros = table.Column<bool>(type: "bit", nullable: true),
                    CuentasBancarias = table.Column<bool>(type: "bit", nullable: true),
                    Domicilios = table.Column<bool>(type: "bit", nullable: true),
                    Expediente = table.Column<bool>(type: "bit", nullable: true),
                    DispositivodeAlmacenamiento = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_PLANTILLAS",
                columns: table => new
                {
                    PlantillaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Ruta = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    CatTipoProductoID = table.Column<int>(type: "int", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__C5DEB58C9A287233", x => x.PlantillaID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO",
                columns: table => new
                {
                    ProductoRecibidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudPFMCFICID = table.Column<int>(type: "int", nullable: false),
                    GeneralesAntecedentesID = table.Column<int>(type: "int", nullable: false),
                    FechaProducto = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoProducto = table.Column<short>(type: "smallint", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO", x => x.ProductoRecibidoID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_SOLICITUD",
                columns: table => new
                {
                    SolicitudPFMCFICID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActosAPFMID = table.Column<int>(type: "int", nullable: true),
                    Oficio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FechaOficio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaTermino = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConDetenido = table.Column<bool>(type: "bit", nullable: true),
                    CatTipoProductoID = table.Column<int>(type: "int", nullable: true),
                    CatTipoEnvioID = table.Column<int>(type: "int", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    FechaSistema = table.Column<DateTime>(type: "datetime", nullable: true),
                    CatEstatusSolicitudID = table.Column<int>(type: "int", nullable: true),
                    PersonalID = table.Column<int>(type: "int", nullable: true),
                    CatTipoMandamientoID = table.Column<int>(type: "int", nullable: true),
                    AdscripcionID = table.Column<int>(type: "int", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__3411BB0B5B5A8B25", x => x.SolicitudPFMCFICID)
                        .Annotation("SqlServer:FillFactor", 80);
                });

            migrationBuilder.CreateTable(
                name: "MotivoRechazo",
                columns: table => new
                {
                    MotivoRechazoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TipoRechazo = table.Column<int>(type: "int", nullable: false),
                    PFM = table.Column<bool>(type: "bit", nullable: true),
                    CGSP = table.Column<bool>(type: "bit", nullable: true),
                    CENAPI = table.Column<bool>(type: "bit", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.MotivoRechazoNueva", x => x.MotivoRechazoId);
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CANCELACIONES",
                columns: table => new
                {
                    CancelacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolictudPFMCFICID = table.Column<int>(type: "int", nullable: true),
                    FechaCancelacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    MotivoCancelacion = table.Column<int>(type: "int", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__5A8447EE5E201ADC", x => x.CancelacionID)
                        .Annotation("SqlServer:FillFactor", 80);
                    table.ForeignKey(
                        name: "FK_INTERFAZ_PFM_CFIC_CANCELACIONES_INTERFAZ_PFM_CFIC_SOLICITUD",
                        column: x => x.SolictudPFMCFICID,
                        principalTable: "INTERFAZ_PFM_CFIC_SOLICITUD",
                        principalColumn: "SolicitudPFMCFICID");
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO",
                columns: table => new
                {
                    ConfirmacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolictudPFMCFICID = table.Column<int>(type: "int", nullable: true),
                    TipoConfirmacion = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true),
                    CodigoRetorno = table.Column<int>(type: "int", nullable: true),
                    Mensaje = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    FolioConfirmacionCFIC = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__637503903EAD6EE9", x => x.ConfirmacionID)
                        .Annotation("SqlServer:FillFactor", 80);
                    table.ForeignKey(
                        name: "FK_INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO_INTERFAZ_PFM_CFIC_SOLICITUD",
                        column: x => x.SolictudPFMCFICID,
                        principalTable: "INTERFAZ_PFM_CFIC_SOLICITUD",
                        principalColumn: "SolicitudPFMCFICID");
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_CFIC_RECHAZOS",
                columns: table => new
                {
                    RechazoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolictudPFMCFICID = table.Column<int>(type: "int", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoRechazoID = table.Column<int>(type: "int", nullable: true),
                    MotivoRechazoID = table.Column<int>(type: "int", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Expirado = table.Column<bool>(type: "bit", nullable: true),
                    FechaAltaDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaActualizacionDelta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__EEE56439E97E3F10", x => x.RechazoID)
                        .Annotation("SqlServer:FillFactor", 80);
                    table.ForeignKey(
                        name: "FK_INTERFAZ_PFM_CFIC_RECHAZOS_INTERFAZ_PFM_CFIC_SOLICITUD",
                        column: x => x.SolictudPFMCFICID,
                        principalTable: "INTERFAZ_PFM_CFIC_SOLICITUD",
                        principalColumn: "SolicitudPFMCFICID");
                });

            migrationBuilder.CreateTable(
                name: "INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION",
                columns: table => new
                {
                    ConfirmacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolictudPFMCFICID = table.Column<int>(type: "int", nullable: true),
                    RegistroID = table.Column<int>(type: "int", nullable: true),
                    TipoConfirmacion = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true),
                    CodigoRetorno = table.Column<int>(type: "int", nullable: true),
                    Mensaje = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    FolioConfirmacionPFM = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INTERFAZ__6375039090C53A46", x => x.ConfirmacionID)
                        .Annotation("SqlServer:FillFactor", 80);
                    table.ForeignKey(
                        name: "FK_INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION_INTERFAZ_PFM_CFIC_SOLICITUD",
                        column: x => x.SolictudPFMCFICID,
                        principalTable: "INTERFAZ_PFM_CFIC_SOLICITUD",
                        principalColumn: "SolicitudPFMCFICID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTERFAZ_PFM_CFIC_CANCELACIONES_SolictudPFMCFICID",
                table: "INTERFAZ_PFM_CFIC_CANCELACIONES",
                column: "SolictudPFMCFICID");

            migrationBuilder.CreateIndex(
                name: "IX_INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO_SolictudPFMCFICID",
                table: "INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO",
                column: "SolictudPFMCFICID");

            migrationBuilder.CreateIndex(
                name: "IX_INTERFAZ_PFM_CFIC_RECHAZOS_SolictudPFMCFICID",
                table: "INTERFAZ_PFM_CFIC_RECHAZOS",
                column: "SolictudPFMCFICID");

            migrationBuilder.CreateIndex(
                name: "IX_INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION_SolictudPFMCFICID",
                table: "INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION",
                column: "SolictudPFMCFICID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_ARCHIVO");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CANCELACIONES");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CatCodigoRetorno");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CatEstatusSolicitud");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CatTipoConfirmacion");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CatTipoProducto");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_PLANTILLAS");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_RECHAZOS");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION");

            migrationBuilder.DropTable(
                name: "MotivoRechazo");

            migrationBuilder.DropTable(
                name: "INTERFAZ_PFM_CFIC_SOLICITUD");
        }
    }
}

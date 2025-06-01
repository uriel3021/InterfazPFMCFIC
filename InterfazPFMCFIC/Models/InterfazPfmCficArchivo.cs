using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficArchivo
{
    public int ArchivoId { get; set; }

    public int? ProcesoId { get; set; }

    public int? RegistroId { get; set; }

    public string? Ruta { get; set; }

    public string? NombreArchivo { get; set; }

    public decimal? PesoArchivoKb { get; set; }

    public bool? Borrado { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }
}

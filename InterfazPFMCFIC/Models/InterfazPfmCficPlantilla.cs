using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficPlantilla
{
    public int PlantillaId { get; set; }

    public string? Nombre { get; set; }

    public string? Ruta { get; set; }

    public int? CatTipoProductoId { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }
}

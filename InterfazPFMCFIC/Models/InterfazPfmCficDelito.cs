using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficDelito
{
    public int DelitoCficpfmid { get; set; }

    public int? SolictudPfmcficid { get; set; }

    public int? TipoDelito { get; set; }

    public int? ClasificacionDelito { get; set; }

    public bool? EsPrincipal { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }
}

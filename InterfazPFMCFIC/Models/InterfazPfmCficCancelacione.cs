using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficCancelacione
{
    public int CancelacionId { get; set; }

    public int? SolictudPfmcficid { get; set; }

    public DateTime? FechaCancelacion { get; set; }

    public int? MotivoCancelacion { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }
}

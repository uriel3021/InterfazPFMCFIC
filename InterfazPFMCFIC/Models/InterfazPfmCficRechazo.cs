using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficRechazo
{
    public int RechazoId { get; set; }

    public int? SolictudPfmcficid { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public int? TipoRechazoId { get; set; }

    public int? MotivoRechazoId { get; set; }

    public string? Observaciones { get; set; }

    public bool? Expirado { get; set; }

 

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }

    public virtual InterfazPfmCficSolicitud? SolictudPfmcfic { get; set; }
}

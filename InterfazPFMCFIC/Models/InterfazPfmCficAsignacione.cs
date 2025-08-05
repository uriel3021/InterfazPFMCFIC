using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficAsignacione
{
    public int AsignacionPfmcficid { get; set; }

    public int SolicitudPfmcficid { get; set; }

    public int PersonalId { get; set; }

    public string NombreAnalista { get; set; } = null!;

    public DateTime FechaAsignacion { get; set; }

    public DateTime FechaAltaDelta { get; set; }

    public DateTime FechaActualizacionDelta { get; set; }

    public bool Borrado { get; set; }

    public virtual InterfazPfmCficSolicitud SolicitudPfmcfic { get; set; } = null!;
}

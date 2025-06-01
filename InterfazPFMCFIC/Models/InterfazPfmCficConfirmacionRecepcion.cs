using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficConfirmacionRecepcion
{
    public int ConfirmacionId { get; set; }

    public int? SolictudPfmcficid { get; set; }

    public int? RegistroId { get; set; }

    public int? TipoConfirmacion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? CodigoRetorno { get; set; }

    public string? Mensaje { get; set; }

    public long? FolioConfirmacionPfm { get; set; }
}

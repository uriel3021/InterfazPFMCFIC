using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class CatMotivoRechazo
{
    public int MotivoRechaId { get; set; }

    public string Motivo { get; set; } = null!;

    public int TipoRechazoId { get; set; }

    public bool Pfm { get; set; }

    public bool Cgsp { get; set; }

    public bool Cenapi { get; set; }

    public bool Borrado { get; set; }
}

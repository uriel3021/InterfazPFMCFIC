using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class MotivoRechazo
{
    public int MotivoRechazoId { get; set; }

    public string Motivo { get; set; } = null!;

    public int TipoRechazo { get; set; }

    public bool? Pfm { get; set; }

    public bool? Cgsp { get; set; }

    public bool? Cenapi { get; set; }

    public bool Borrado { get; set; }
}

using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficProductorecibido
{
    public int ProductoRecibidoId { get; set; }

    public int SolicitudPfmcficid { get; set; }

    public int GeneralesAntecedentesId { get; set; }

    public DateTime? FechaProducto { get; set; }

    public short? TipoProducto { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }

    // Nueva relación
    public int CatTipoConfirmacionId { get; set; }
    public virtual InterfazPfmCficCatTipoConfirmacion CatTipoConfirmacion { get; set; } = null!;
}

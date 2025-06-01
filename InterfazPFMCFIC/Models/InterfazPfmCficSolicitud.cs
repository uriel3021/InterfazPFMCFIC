using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficSolicitud
{
    public int SolictudPfmcficid { get; set; }

    public int? ActoId { get; set; }

    public int? SoliictudIdbts { get; set; }

    public int? CatMandamientoId { get; set; }

    public int? CatEstatusSolicitudId { get; set; }

    public int? SubAreaOrigenId { get; set; }

    public int? CatTipoExpedienteId { get; set; }

    public string? OficioRemision { get; set; }

    public int? CatTipoProductoId { get; set; }

    public string? NumeroExpediente { get; set; }

    public DateTime? FechaOficio { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public DateTime? FechaTermino { get; set; }

    public bool? ConDetenido { get; set; }

    public string? NombreAutoridad { get; set; }

    public string? Observaciones { get; set; }

    public int? AnioFolioSiga { get; set; }

    public long? NumeroFolioSiga { get; set; }

    public bool? TieneAnexos { get; set; }

    public bool? Borrado { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }
}

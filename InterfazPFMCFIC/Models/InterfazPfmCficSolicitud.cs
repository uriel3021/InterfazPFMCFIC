using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficSolicitud
{
    public int SolicitudPfmcficid { get; set; }

    public int? ActosApfmid { get; set; }

    public string? Oficio { get; set; }

    public DateTime? FechaOficio { get; set; }

    public DateTime? FechaTermino { get; set; }

    public bool? ConDetenido { get; set; }

    public int? CatTipoProductoId { get; set; }

    public int? CatTipoEnvioId { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaSistema { get; set; }

    public int? CatEstatusSolicitudId { get; set; }

    public int? PersonalId { get; set; }

    public int? CatTipoMandamientoId { get; set; }

    public int? AdscripcionId { get; set; }

    public string? UsuarioId { get; set; }

    public DateTime? FechaAltaDelta { get; set; }

    public DateTime? FechaActualizacionDelta { get; set; }

    public bool? Borrado { get; set; }

    public virtual ICollection<InterfazPfmCficCancelacione> InterfazPfmCficCancelaciones { get; set; } = new List<InterfazPfmCficCancelacione>();

    public virtual ICollection<InterfazPfmCficConfirmacionEnvio> InterfazPfmCficConfirmacionEnvios { get; set; } = new List<InterfazPfmCficConfirmacionEnvio>();

    public virtual ICollection<InterfazPfmCficRechazo> InterfazPfmCficRechazos { get; set; } = new List<InterfazPfmCficRechazo>();

    public virtual ICollection<InterfazPfmPfmCficConfirmacionRecepcion> InterfazPfmPfmCficConfirmacionRecepcions { get; set; } = new List<InterfazPfmPfmCficConfirmacionRecepcion>();
}


public enum TipoConfirmacion
{
    Solicitud = 1,
    Aceptacion = 10,
    Rechazo = 8,
    InformeParcial = 15,
    InformeTotal = 2,
    Cancelacion = 4,
    Actualizacion = 6
}
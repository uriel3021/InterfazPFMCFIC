// ViewModels/MovimientoTablaViewModel.cs
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.ViewModels;

public class MovimientoTablaViewModel
{
    public string Tipo { get; set; }
    public DateTime? Fecha { get; set; }
    public long? Folio { get; set; }
    public int ArchivoId { get; set; } // <--- Agrega esto
    public string? ArchivoNombre { get; set; } // <--- Y esto

    // Solo para rechazos
    public int? TipoRechazoId { get; set; }
    public int? MotivoRechazoId { get; set; }
    public string? ObservacionesRechazo { get; set; }
    public string? MotivoRechazoTexto { get; set; }
    public string? TipoRechazoTexto { get; set; }

    //solo para asignaciones
    public string NombreAnalista { get; set; } = null!;
    public DateTime FechaAsignacion { get; set; }

}
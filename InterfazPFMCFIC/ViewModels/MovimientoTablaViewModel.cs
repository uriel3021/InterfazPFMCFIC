// ViewModels/MovimientoTablaViewModel.cs
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.ViewModels;

public class MovimientoTablaViewModel
{
    public TipoConfirmacion Tipo { get; set; }
    public DateTime? Fecha { get; set; }
    public string? Mensaje { get; set; }
    public int ArchivoId { get; set; } // <--- Agrega esto
    public string? ArchivoNombre { get; set; } // <--- Y esto
}
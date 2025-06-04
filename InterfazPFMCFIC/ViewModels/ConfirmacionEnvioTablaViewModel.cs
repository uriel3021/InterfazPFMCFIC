namespace InterfazPFMCFIC.ViewModels;

public class ConfirmacionEnvioTablaViewModel
{
    public EstatusEnvio? Estatus { get; set; }
    public DateTime? Fecha { get; set; }
    public long? Folio { get; set; }
    public string? ArchivoNombre { get; set; } // Solo el nombre, sin extensión
    public string? ArchivoExtension { get; set; } // Solo la extensión, ej: ".pdf"
    public int ArchivoId { get; set; }
}

public enum EstatusEnvio
{
    Pendiente = 0,
    Enviado = 1,
    Confirmado = 2,
    Rechazado = 3,
    Error = 4
}
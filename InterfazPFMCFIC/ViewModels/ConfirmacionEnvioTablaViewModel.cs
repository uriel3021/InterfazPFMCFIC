namespace InterfazPFMCFIC.ViewModels;

public class ConfirmacionEnvioTablaViewModel
{
    public EstatusEnvio? Estatus { get; set; }
    public DateTime? Fecha { get; set; }
    public long? Folio { get; set; }
    public string? Archivo { get; set; }
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
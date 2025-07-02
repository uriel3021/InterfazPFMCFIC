using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

public class TestPlantillaModel : PageModel
{
    private readonly PlantillaService _plantillaService;

    public TestPlantillaModel(PlantillaService plantillaService)
    {
        _plantillaService = plantillaService;
    }

    public IActionResult OnPostGenerarPlantilla1()
    {
        // JSON con los valores de los marcadores
        var jsonValores = JsonSerializer.Serialize(new
        {
            Dependencia_Solicitante = "Fiscal�a General de Justicia",
            Subdependencia_solicitante = "Direcci�n de Investigaci�n Criminal",
            Numero_Oficio = "12345/2025",
            Exp_Ministerial_vinculado_a_la_solicitud = "EXP-2025-001",
            Fecha = "Ciudad de M�xico, 01 de enero de 2025",
            Numero_Carpeta = "FGJ-12345",
            Delito = "Robo agravado",
            NOMBRE_COMPLETO = "Juan P�rez L�pez",
            CARGO = "Director General"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("1_Plantilla_Ficha T�cnica de Identificaci�n.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Ficha_Tecnica_Identificacion.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }
}
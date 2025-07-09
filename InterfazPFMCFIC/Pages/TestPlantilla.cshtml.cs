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

    public IActionResult OnPostGenerarPlantilla2()
    {
        // JSON con los valores de los marcadores para la plantilla 2
        var jsonValores = JsonSerializer.Serialize(new
        {
            JEFATURA_ESTATAL_O_DIRECCION_GENERAL = "Fiscal�a General de Justicia del Estado de M�xico",
            NUMERO_DE_OFICIO = "FGJ/DGAI/567/2025",
            CARPETA_DE_INVESTIGACION = "FGJ-CI-2025-001234",
            FECHA_DE_ENVIO_POR_SISTEMA = "15 de enero de 2025",
            DELITO_PRINCIPAL = "Homicidio doloso agravado",
            fundamentar_y_motivar_la_peticion = "Con fundamento en los art�culos 16 y 21 de la Constituci�n Pol�tica de los Estados Unidos Mexicanos, y en ejercicio de las facultades conferidas por la Ley Org�nica de la Fiscal�a General de Justicia, se solicita informaci�n de antecedentes penales para el esclarecimiento de los hechos investigados en la carpeta de investigaci�n antes mencionada.",
            NOMBRE_DEL_AGENTE_DE_LA_PFM = "Lic. Mar�a Elena Rodr�guez Hern�ndez",
            CARGO_DEL_AGENTE_DE_LA_PFM = "Agente del Ministerio P�blico Especializado",
            // Nuevos marcadores agregados
            direccion_de_recepcion = "Av. Constituyentes No. 947, Col. Bel�n, C.P. 50130, Toluca de Lerdo, Estado de M�xico",
            correo_electronico = "antecedentes.pfm@fgjem.gob.mx",
            proporcionar_numero = "722 226 1600",
            proporcionar_extension = "Ext. 8520"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("2_Plantilla_Ficha de Antecedentes Ministeriales.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Ficha de Antecedentes Ministeriales.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    public IActionResult OnPostGenerarPlantilla3()
    {
        // JSON con los valores de los marcadores para la plantilla 3
        var jsonValores = JsonSerializer.Serialize(new
        {
            JEFATURA_ESTATAL_O_DIRECCION_GENERAL = "Fiscal�a General de Justicia del Estado de Jalisco",
            NUMERO_DE_OFICIO = "FGJ/JAL/789/2025",
            CARPETA_DE_INVESTIGACION = "FGJ-JAL-2025-005678",
            FECHA_DE_ENVIO_POR_SISTEMA = "20 de enero de 2025",
            DELITO_PRINCIPAL = "Secuestro agravado",
            direccion_de_recepcion = "Av. Alcalde No. 1351, Col. Miraflores, C.P. 44270, Guadalajara, Jalisco",
            correo_electronico = "solicitudes.cfic@fgjjalisco.gob.mx",
            proporcionar_numero = "33 3030 8000",
            proporcionar_extension = "Ext. 4525",
            NOMBRE_DEL_AGENTE_DE_LA_PFM = "Lic. Carlos Antonio Mendoza Silva",
            CARGO_DEL_AGENTE_DE_LA_PFM = "Fiscal Especializado en Investigaci�n"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("3_Plantilla_Ficha T�cnica de Identificaci�n con Detenido.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Ficha Tecnica de Identificacion con Detenido.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    public IActionResult OnPostGenerarPlantilla4()
    {
        // JSON con los valores de los marcadores para la plantilla 4
        var jsonValores = JsonSerializer.Serialize(new
        {
            JEFATURA_ESTATAL_O_DIRECCION_GENERAL = "Fiscal�a General de Justicia del Estado de Nuevo Le�n",
            NUMERO_DE_OFICIO = "FGJ/NL/2025/1001",
            CARPETA_DE_INVESTIGACION = "FGJ-NL-CI-2025-009876",
            FECHA_DE_ENVIO_POR_SISTEMA = "25 de enero de 2025",
            DELITO_PRINCIPAL = "Extorsi�n agravada",
            direccion_de_recepcion = "Washington No. 2000, Col. Obrera, C.P. 64010, Monterrey, Nuevo Le�n",
            correo_electronico = "cfic.solicitudes@fgjnl.gob.mx",
            proporcionar_numero = "81 2020 9090",
            proporcionar_extension = "Ext. 3315",
            NOMBRE_DEL_AGENTE_DE_LA_PFM = "Lic. Ana Sof�a Mart�nez Gonz�lez",
            CARGO_DEL_AGENTE_DE_LA_PFM = "Fiscal de Investigaci�n Especializada"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("4_Plantilla_Ficha de Fuentes Abiertas.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Ficha de Fuentes Abiertas.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    public IActionResult OnPostGenerarPlantilla5()
    {
        // JSON con los valores de los marcadores para la plantilla 5
        var jsonValores = JsonSerializer.Serialize(new
        {
            JEFATURA_ESTATAL_O_DIRECCION_GENERAL = "Fiscal�a General de Justicia del Estado de Veracruz",
            NUMERO_DE_OFICIO = "FGJ/VER/2025/2500",
            CARPETA_DE_INVESTIGACION = "FGJ-VER-CI-2025-012345",
            FECHA_DE_ENVIO_POR_SISTEMA = "30 de enero de 2025",
            DELITO_PRINCIPAL = "Trata de personas",
            direccion_de_recepcion = "Calle �rsulo Galv�n No. 8, Col. Centro, C.P. 91000, Xalapa-Enr�quez, Veracruz",
            correo_electronico = "antecedentes.cfic@fiscaliaveracruz.gob.mx",
            proporcionar_numero = "228 841 7400",
            proporcionar_extension = "Ext. 2150",
            NOMBRE_DEL_AGENTE_DE_LA_PFM = "Lic. Roberto Alejandro Hern�ndez Castro",
            CARGO_DEL_AGENTE_DE_LA_PFM = "Fiscal Regional Especializado"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("5_Plantilla_C�rculo de Proximidad.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Circulo de Proximidad.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    public IActionResult OnPostGenerarPlantilla6()
    {
        // JSON con los valores de los marcadores para la plantilla 6
        var jsonValores = JsonSerializer.Serialize(new
        {
            JEFATURA_ESTATAL_O_DIRECCION_GENERAL = "Fiscal�a General de Justicia del Estado de Puebla",
            NUMERO_DE_OFICIO = "FGJ/PUE/2025/3500",
            CARPETA_DE_INVESTIGACION = "FGJ-PUE-CI-2025-098765",
            FECHA_DE_ENVIO_POR_SISTEMA = "05 de febrero de 2025",
            DELITO_PRINCIPAL = "Feminicidio",
            direccion_de_recepcion = "Calle 2 Sur No. 2503, Col. El Carmen, C.P. 72000, Puebla de Zaragoza, Puebla",
            correo_electronico = "solicitudes.antecedentes@fiscaliapuebla.gob.mx",
            proporcionar_numero = "222 229 6200",
            proporcionar_extension = "Ext. 1750",
            NOMBRE_DEL_AGENTE_DE_LA_PFM = "Lic. Diana Patricia Morales V�zquez",
            CARGO_DEL_AGENTE_DE_LA_PFM = "Fiscal Especializada en Delitos de G�nero"
        });

        // Llenar la plantilla
        byte[] archivoGenerado = _plantillaService.LlenarPlantilla("6_Plantilla_Ficha T�cnica de Identificacion de Armas de fuego.docx", jsonValores);

        // Devolver el archivo generado para abrirlo directamente en Word
        Response.Headers["Content-Disposition"] = "inline; filename=Ficha Tecnica de Identificacion de Armas de fuego.docx";
        return File(archivoGenerado, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InterfazPFMCFIC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaService _plantillaService;
    public PlantillaController(PlantillaService plantillaService)
    {
        _plantillaService = plantillaService;
    }

    /// <summary>
    /// Genera una plantilla Word a partir del nombre de plantilla y un JSON con los valores de los marcadores.
    /// Ejemplo de uso para cada plantilla:
    /// 
    /// 1_Plantilla_Ficha Técnica de Identificación.docx
    /// {
    ///   "Dependencia_Solicitante": "Fiscalía General de Justicia",
    ///   "Subdependencia_solicitante": "Dirección de Investigación Criminal",
    ///   "Numero_Oficio": "12345/2025",
    ///   "Exp_Ministerial_vinculado_a_la_solicitud": "EXP-2025-001",
    ///   "Fecha": "Ciudad de México, 01 de enero de 2025",
    ///   "Numero_Carpeta": "FGJ-12345",
    ///   "Delito": "Robo agravado",
    ///   "NOMBRE_COMPLETO": "Juan Pérez López",
    ///   "CARGO": "Director General"
    /// }
    /// 
    /// 2_Plantilla_Ficha de Antecedentes Ministeriales.docx
    /// {
    ///   "JEFATURA_ESTATAL_O_DIRECCION_GENERAL": "Fiscalía General de Justicia del Estado de México",
    ///   "NUMERO_DE_OFICIO": "FGJ/DGAI/567/2025",
    ///   "CARPETA_DE_INVESTIGACION": "FGJ-CI-2025-001234",
    ///   "FECHA_DE_ENVIO_POR_SISTEMA": "15 de enero de 2025",
    ///   "DELITO_PRINCIPAL": "Homicidio doloso agravado",
    ///   "fundamentar_y_motivar_la_peticion": "...",
    ///   "NOMBRE_DEL_AGENTE_DE_LA_PFM": "Lic. María Elena Rodríguez Hernández",
    ///   "CARGO_DEL_AGENTE_DE_LA_PFM": "Agente del Ministerio Público Especializado",
    ///   "direccion_de_recepcion": "Av. Constituyentes No. 947, Col. Belén, C.P. 50130, Toluca de Lerdo, Estado de México",
    ///   "correo_electronico": "antecedentes.pfm@fgjem.gob.mx",
    ///   "proporcionar_numero": "722 226 1600",
    ///   "proporcionar_extension": "Ext. 8520"
    /// }
    /// 
    /// 3_Plantilla_Ficha Técnica de Identificación con Detenido.docx
    /// {
    ///   "JEFATURA_ESTATAL_O_DIRECCION_GENERAL": "Fiscalía General de Justicia del Estado de Jalisco",
    ///   "NUMERO_DE_OFICIO": "FGJ/JAL/789/2025",
    ///   "CARPETA_DE_INVESTIGACION": "FGJ-JAL-2025-005678",
    ///   "FECHA_DE_ENVIO_POR_SISTEMA": "20 de enero de 2025",
    ///   "DELITO_PRINCIPAL": "Secuestro agravado",
    ///   "direccion_de_recepcion": "Av. Alcalde No. 1351, Col. Miraflores, C.P. 44270, Guadalajara, Jalisco",
    ///   "correo_electronico": "solicitudes.cfic@fgjjalisco.gob.mx",
    ///   "proporcionar_numero": "33 3030 8000",
    ///   "proporcionar_extension": "Ext. 4525",
    ///   "NOMBRE_DEL_AGENTE_DE_LA_PFM": "Lic. Carlos Antonio Mendoza Silva",
    ///   "CARGO_DEL_AGENTE_DE_LA_PFM": "Fiscal Especializado en Investigación"
    /// }
    /// 
    /// 4_Plantilla_Ficha de Fuentes Abiertas.docx
    /// {
    ///   "JEFATURA_ESTATAL_O_DIRECCION_GENERAL": "Fiscalía General de Justicia del Estado de Nuevo León",
    ///   "NUMERO_DE_OFICIO": "FGJ/NL/2025/1001",
    ///   "CARPETA_DE_INVESTIGACION": "FGJ-NL-CI-2025-009876",
    ///   "FECHA_DE_ENVIO_POR_SISTEMA": "25 de enero de 2025",
    ///   "DELITO_PRINCIPAL": "Extorsión agravada",
    ///   "direccion_de_recepcion": "Washington No. 2000, Col. Obrera, C.P. 64010, Monterrey, Nuevo León",
    ///   "correo_electronico": "cfic.solicitudes@fgjnl.gob.mx",
    ///   "proporcionar_numero": "81 2020 9090",
    ///   "proporcionar_extension": "Ext. 3315",
    ///   "NOMBRE_DEL_AGENTE_DE_LA_PFM": "Lic. Ana Sofía Martínez González",
    ///   "CARGO_DEL_AGENTE_DE_LA_PFM": "Fiscal de Investigación Especializada"
    /// }
    /// 
    /// 5_Plantilla_Círculo de Proximidad.docx
    /// {
    ///   "JEFATURA_ESTATAL_O_DIRECCION_GENERAL": "Fiscalía General de Justicia del Estado de Veracruz",
    ///   "NUMERO_DE_OFICIO": "FGJ/VER/2025/2500",
    ///   "CARPETA_DE_INVESTIGACION": "FGJ-VER-CI-2025-012345",
    ///   "FECHA_DE_ENVIO_POR_SISTEMA": "30 de enero de 2025",
    ///   "DELITO_PRINCIPAL": "Trata de personas",
    ///   "direccion_de_recepcion": "Calle Úrsulo Galván No. 8, Col. Centro, C.P. 91000, Xalapa-Enríquez, Veracruz",
    ///   "correo_electronico": "antecedentes.cfic@fiscaliaveracruz.gob.mx",
    ///   "proporcionar_numero": "228 841 7400",
    ///   "proporcionar_extension": "Ext. 2150",
    ///   "NOMBRE_DEL_AGENTE_DE_LA_PFM": "Lic. Roberto Alejandro Hernández Castro",
    ///   "CARGO_DEL_AGENTE_DE_LA_PFM": "Fiscal Regional Especializado"
    /// }
    /// 
    /// 6_Plantilla_Ficha Técnica de Identificacion de Armas de fuego.docx
    /// {
    ///   "JEFATURA_ESTATAL_O_DIRECCION_GENERAL": "Fiscalía General de Justicia del Estado de Puebla",
    ///   "NUMERO_DE_OFICIO": "FGJ/PUE/2025/3500",
    ///   "CARPETA_DE_INVESTIGACION": "FGJ-PUE-CI-2025-098765",
    ///   "FECHA_DE_ENVIO_POR_SISTEMA": "05 de febrero de 2025",
    ///   "DELITO_PRINCIPAL": "Feminicidio",
    ///   "direccion_de_recepcion": "Calle 2 Sur No. 2503, Col. El Carmen, C.P. 72000, Puebla de Zaragoza, Puebla",
    ///   "correo_electronico": "solicitudes.antecedentes@fiscaliapuebla.gob.mx",
    ///   "proporcionar_numero": "222 229 6200",
    ///   "proporcionar_extension": "Ext. 1750",
    ///   "NOMBRE_DEL_AGENTE_DE_LA_PFM": "Lic. Diana Patricia Morales Vázquez",
    ///   "CARGO_DEL_AGENTE_DE_LA_PFM": "Fiscal Especializada en Delitos de Género"
    /// }
    /// </summary>
    /// <param name="plantilla">Nombre del archivo de plantilla (ejemplo: 1_Plantilla_Ficha Técnica de Identificación.docx)</param>
    /// <param name="jsonValores">JSON con los valores de los marcadores</param>
    /// <returns>Archivo Word generado</returns>
    [HttpPost("generar")]
    public IActionResult GenerarPlantilla([FromQuery] string plantilla, [FromBody] JsonElement jsonValores)
    {
        if (string.IsNullOrWhiteSpace(plantilla))
            return BadRequest("El parámetro 'plantilla' es obligatorio.");

        string jsonString = jsonValores.GetRawText();
        byte[] archivo = _plantillaService.LlenarPlantilla(plantilla, jsonString);
        return File(archivo, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", plantilla);
    }
}

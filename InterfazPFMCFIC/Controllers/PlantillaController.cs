using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using InterfazPFMCFIC.Infrastructure;
using Ardalis.Specification;

namespace InterfazPFMCFIC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaService _plantillaService;
    private readonly IRepositoryBase<InterfazPfmCficPlantilla> _repoPlantilla;

    public PlantillaController(PlantillaService plantillaService, IRepositoryBase<InterfazPfmCficPlantilla> repoPlantilla)
    {
        _plantillaService = plantillaService;
        _repoPlantilla = repoPlantilla;
    }

    /// <summary>
    /// Genera una plantilla Word a partir del CatTipoProductoId y un JSON con los valores de los marcadores.
    /// </summary>
    /// <param name="catTipoProductoId">ID del tipo de producto de la plantilla</param>
    /// <param name="jsonValores">JSON con los valores de los marcadores</param>
    /// <returns>Archivo Word generado</returns>
    [HttpPost("generar")]
    public async Task<IActionResult> GenerarPlantilla([FromQuery] int catTipoProductoId, [FromBody] JsonElement jsonValores)
    {
        var spec = new PlantillaPorTipoProductoSpec(catTipoProductoId);
        var plantilla = await _repoPlantilla.FirstOrDefaultAsync(spec);
        if (plantilla == null || string.IsNullOrWhiteSpace(plantilla.Nombre))
            return BadRequest("No se encontró la plantilla para el CatTipoProductoId proporcionado.");

        string jsonString = jsonValores.GetRawText();
        byte[] archivo = _plantillaService.LlenarPlantilla(plantilla.Nombre, jsonString);
        return File(archivo, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", plantilla.Nombre);
    }
}

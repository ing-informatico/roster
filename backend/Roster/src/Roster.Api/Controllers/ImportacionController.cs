using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roster.Application.Importacion;
using Roster.Infrastructure.Importacion;

namespace Roster.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/importacion")]
public class ImportacionController : ControllerBase
{
    private readonly ExcelEmpleadoImporter _importer;

    public ImportacionController(ExcelEmpleadoImporter importer)
    {
        _importer = importer;
    }

    [Authorize(Policy = "CanEdit")]
    [HttpPost("empleados")]
    public async Task<ActionResult<ImportResultDto>> ImportEmpleados(IFormFile archivo)
    {
        if (archivo is null || archivo.Length == 0)
            return BadRequest(new { error = "No se recibio ningun archivo." });

        var nombre = archivo.FileName.ToLowerInvariant();
        if (!nombre.EndsWith(".xlsx") && !nombre.EndsWith(".xls"))
            return BadRequest(new { error = "El archivo debe ser un Excel (.xlsx)." });

        await using var stream = archivo.OpenReadStream();
        var result = await _importer.ImportAsync(stream);
        return Ok(result);
    }
}

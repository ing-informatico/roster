using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Empleados;
using Roster.Infrastructure.Persistence;

namespace Roster.Api.Controllers;

[ApiController]
[Route("api/empleados")]
public class EmpleadosController : ControllerBase
{
    private readonly RosterDbContext _db;

    public EmpleadosController(RosterDbContext db)
    {
        _db = db;
    }

    /// <summary>Returns the list of employees.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmpleadoListItemDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var empleados = await _db.Empleados
            .AsNoTracking()
            .OrderBy(e => e.NombreCompleto)
            .Select(e => new EmpleadoListItemDto
            {
                Id = e.Id,
                Codigo = e.Codigo,
                NombreCompleto = e.NombreCompleto,
                Correo = e.Correo,
                Departamento = e.Departamento != null ? e.Departamento.Nombre : null,
                Pais = e.Pais != null ? e.Pais.Nombre : null,
                Activo = e.Activo
            })
            .ToListAsync(cancellationToken);

        return Ok(empleados);
    }
}

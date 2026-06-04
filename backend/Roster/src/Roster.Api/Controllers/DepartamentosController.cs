using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Catalogos;
using Roster.Infrastructure.Persistence;

namespace Roster.Api.Controllers;

[ApiController]
[Route("api/departamentos")]
public class DepartamentosController : ControllerBase
{
    private readonly RosterDbContext _db;

    public DepartamentosController(RosterDbContext db)
    {
        _db = db;
    }

    /// <summary>Returns the department catalog.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CatalogoItemDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _db.Departamentos
            .AsNoTracking()
            .OrderBy(d => d.Nombre)
            .Select(d => new CatalogoItemDto { Id = d.Id, Nombre = d.Nombre })
            .ToListAsync(cancellationToken);

        return Ok(items);
    }
}

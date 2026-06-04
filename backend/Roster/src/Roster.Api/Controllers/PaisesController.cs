using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Catalogos;
using Roster.Infrastructure.Persistence;

namespace Roster.Api.Controllers;

[ApiController]
[Route("api/paises")]
public class PaisesController : ControllerBase
{
    private readonly RosterDbContext _db;

    public PaisesController(RosterDbContext db)
    {
        _db = db;
    }

    /// <summary>Returns the country catalog.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CatalogoItemDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var items = await _db.Paises
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .Select(p => new CatalogoItemDto { Id = p.Id, Nombre = p.Nombre })
            .ToListAsync(cancellationToken);

        return Ok(items);
    }
}

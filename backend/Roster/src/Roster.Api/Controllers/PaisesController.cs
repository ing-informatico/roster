using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Catalogos;
using Roster.Domain.Entities;
using Roster.Infrastructure.Persistence;

namespace Roster.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/paises")]
public class PaisesController : ControllerBase
{
    private readonly RosterDbContext _db;

    public PaisesController(RosterDbContext db)
    {
        _db = db;
    }

    /// <summary>Lists all countries ordered by name.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogoItemDto>>> GetAll()
    {
        var items = await _db.Paises
            .OrderBy(p => p.Nombre)
            .Select(p => new CatalogoItemDto { Id = p.Id, Nombre = p.Nombre })
            .ToListAsync();
        return Ok(items);
    }

    /// <summary>Creates a new country. Requires Editor role.</summary>
    [Authorize(Policy = "CanEdit")]
    [HttpPost]
    public async Task<ActionResult<CatalogoItemDto>> Create([FromBody] GuardarCatalogoDto dto)
    {
        var nombre = dto.Nombre?.Trim() ?? string.Empty;
        if (nombre.Length < 2)
            return BadRequest(new { error = "El nombre es obligatorio (minimo 2 caracteres)." });

        var exists = await _db.Paises.AnyAsync(p => p.Nombre == nombre);
        if (exists)
            return Conflict(new { error = "Ya existe un pais con ese nombre." });

        var entity = new Pais { Nombre = nombre };
        _db.Paises.Add(entity);
        await _db.SaveChangesAsync();

        var result = new CatalogoItemDto { Id = entity.Id, Nombre = entity.Nombre };
        return CreatedAtAction(nameof(GetAll), new { id = entity.Id }, result);
    }

    /// <summary>Updates a country name. Requires Editor role.</summary>
    [Authorize(Policy = "CanEdit")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(short id, [FromBody] GuardarCatalogoDto dto)
    {
        var entity = await _db.Paises.FindAsync(id);
        if (entity is null) return NotFound();

        var nombre = dto.Nombre?.Trim() ?? string.Empty;
        if (nombre.Length < 2)
            return BadRequest(new { error = "El nombre es obligatorio (minimo 2 caracteres)." });

        var exists = await _db.Paises.AnyAsync(p => p.Nombre == nombre && p.Id != id);
        if (exists)
            return Conflict(new { error = "Ya existe un pais con ese nombre." });

        entity.Nombre = nombre;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Deletes a country if no employees reference it. Requires Editor role.</summary>
    [Authorize(Policy = "CanEdit")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(short id)
    {
        var entity = await _db.Paises.FindAsync(id);
        if (entity is null) return NotFound();

        var inUse = await _db.Empleados.AnyAsync(e => e.PaisId == id);
        if (inUse)
            return Conflict(new { error = "No se puede eliminar: hay empleados en este pais." });

        _db.Paises.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

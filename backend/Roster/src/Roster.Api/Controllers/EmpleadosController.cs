using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Empleados;
using Roster.Domain.Entities;
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

    /// <summary>Creates a new employee.</summary>
    [HttpPost]
    public async Task<ActionResult<EmpleadoListItemDto>> Create(
        [FromBody] CrearEmpleadoDto dto,
        CancellationToken cancellationToken)
    {
        // Server-side validation (source of truth).
        if (string.IsNullOrWhiteSpace(dto.Codigo))
            ModelState.AddModelError(nameof(dto.Codigo), "El codigo es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.NombreCompleto) || dto.NombreCompleto.Trim().Length < 5)
            ModelState.AddModelError(nameof(dto.NombreCompleto), "El nombre es obligatorio (minimo 5 caracteres).");

        if (!string.IsNullOrWhiteSpace(dto.Correo) && !dto.Correo.Contains('@'))
            ModelState.AddModelError(nameof(dto.Correo), "El correo no es valido.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var codigoExiste = await _db.Empleados
            .AnyAsync(e => e.Codigo == dto.Codigo, cancellationToken);
        if (codigoExiste)
        {
            ModelState.AddModelError(nameof(dto.Codigo), "Ese codigo ya existe.");
            return ValidationProblem(ModelState);
        }

        var empleado = new Empleado
        {
            Codigo = dto.Codigo.Trim(),
            NombreCompleto = dto.NombreCompleto.Trim(),
            Correo = dto.Correo?.Trim(),
            FechaIngreso = dto.FechaIngreso,
            FechaNacimiento = dto.FechaNacimiento,
            PaisId = dto.PaisId,
            DepartamentoId = dto.DepartamentoId,
            Activo = true
        };

        _db.Empleados.Add(empleado);
        await _db.SaveChangesAsync(cancellationToken);

        var result = new EmpleadoListItemDto
        {
            Id = empleado.Id,
            Codigo = empleado.Codigo,
            NombreCompleto = empleado.NombreCompleto,
            Correo = empleado.Correo,
            Activo = empleado.Activo
        };

        return CreatedAtAction(nameof(GetAll), new { id = empleado.Id }, result);
    }
}

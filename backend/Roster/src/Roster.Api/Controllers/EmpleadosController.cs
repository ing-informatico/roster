using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roster.Application.Empleados;
using Roster.Domain.Entities;
using Roster.Infrastructure.Persistence;

namespace Roster.Api.Controllers;

[Authorize]
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

    /// <summary>Returns a single employee by id.</summary>
    [HttpGet("{id:long}")]
    public async Task<ActionResult<EmpleadoDetalleDto>> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var empleado = await _db.Empleados
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Select(e => new EmpleadoDetalleDto
            {
                Id = e.Id,
                Codigo = e.Codigo,
                NombreCompleto = e.NombreCompleto,
                Correo = e.Correo,
                Direccion = e.Direccion,
                Telefono1 = e.Telefono1,
                Telefono2 = e.Telefono2,
                FechaNacimiento = e.FechaNacimiento,
                Puesto = e.Puesto,
                Modalidad = e.Modalidad,
                JefeInmediato = e.JefeInmediato,
                Facturable = e.Facturable,
                FechaIngreso = e.FechaIngreso,
                Activo = e.Activo,
                SalarioActual = e.SalarioActual,
                Moneda = e.Moneda,
                Departamento = e.Departamento != null ? e.Departamento.Nombre : null,
                Pais = e.Pais != null ? e.Pais.Nombre : null
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (empleado is null)
            return NotFound();

        return Ok(empleado);
    }

    /// <summary>Creates a new employee. Requires Editor role.</summary>
    [Authorize(Policy = "CanEdit")]
    [HttpPost]
    public async Task<ActionResult<EmpleadoListItemDto>> Create(
        [FromBody] CrearEmpleadoDto dto,
        CancellationToken cancellationToken)
    {
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
            Direccion = dto.Direccion?.Trim(),
            Telefono1 = dto.Telefono1?.Trim(),
            Telefono2 = dto.Telefono2?.Trim(),
            FechaNacimiento = dto.FechaNacimiento,
            Puesto = dto.Puesto?.Trim(),
            Modalidad = dto.Modalidad?.Trim(),
            JefeInmediato = dto.JefeInmediato?.Trim(),
            Facturable = dto.Facturable,
            FechaIngreso = dto.FechaIngreso,
            SalarioActual = dto.SalarioActual,
            Moneda = string.IsNullOrWhiteSpace(dto.Moneda) ? "USD" : dto.Moneda.Trim(),
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

        return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, result);
    }

    /// <summary>Updates an existing employee. Codigo cannot be changed. Requires Editor role.</summary>
    [Authorize(Policy = "CanEdit")]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] ActualizarEmpleadoDto dto,
        CancellationToken cancellationToken)
    {
        var empleado = await _db.Empleados
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (empleado is null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(dto.NombreCompleto) || dto.NombreCompleto.Trim().Length < 5)
            ModelState.AddModelError(nameof(dto.NombreCompleto), "El nombre es obligatorio (minimo 5 caracteres).");

        if (!string.IsNullOrWhiteSpace(dto.Correo) && !dto.Correo.Contains('@'))
            ModelState.AddModelError(nameof(dto.Correo), "El correo no es valido.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        empleado.NombreCompleto = dto.NombreCompleto.Trim();
        empleado.Correo = dto.Correo?.Trim();
        empleado.Direccion = dto.Direccion?.Trim();
        empleado.Telefono1 = dto.Telefono1?.Trim();
        empleado.Telefono2 = dto.Telefono2?.Trim();
        empleado.FechaNacimiento = dto.FechaNacimiento;
        empleado.Puesto = dto.Puesto?.Trim();
        empleado.Modalidad = dto.Modalidad?.Trim();
        empleado.JefeInmediato = dto.JefeInmediato?.Trim();
        empleado.Facturable = dto.Facturable;
        empleado.FechaIngreso = dto.FechaIngreso;
        empleado.SalarioActual = dto.SalarioActual;
        empleado.Moneda = string.IsNullOrWhiteSpace(dto.Moneda) ? "USD" : dto.Moneda.Trim();
        empleado.PaisId = dto.PaisId;
        empleado.DepartamentoId = dto.DepartamentoId;
        empleado.Activo = dto.Activo;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}

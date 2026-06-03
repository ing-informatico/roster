namespace Roster.Domain.Entities;

/// <summary>Core employee entity.</summary>
public class Empleado
{
    public long Id { get; set; }

    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public bool Activo { get; set; } = true;

    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }

    public Pais? Pais { get; set; }
    public Departamento? Departamento { get; set; }
}

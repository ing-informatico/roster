namespace Roster.Application.Empleados;

/// <summary>Read model for a single employee detail.</summary>
public class EmpleadoDetalleDto
{
    public long Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Departamento { get; set; }
    public string? Pais { get; set; }
    public bool Activo { get; set; }
}

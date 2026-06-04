namespace Roster.Application.Empleados;

/// <summary>Write model for updating an employee. Codigo is immutable.</summary>
public class ActualizarEmpleadoDto
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }
    public bool Activo { get; set; }
}

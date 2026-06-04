namespace Roster.Application.Empleados;

/// <summary>Write model for creating an employee.</summary>
public class CrearEmpleadoDto
{
    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }
}

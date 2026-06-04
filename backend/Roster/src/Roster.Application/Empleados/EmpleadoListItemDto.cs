namespace Roster.Application.Empleados;

/// <summary>Read model for listing employees.</summary>
public class EmpleadoListItemDto
{
    public long Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public string? Departamento { get; set; }
    public string? Pais { get; set; }
    public bool Activo { get; set; }
}

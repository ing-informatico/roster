namespace Roster.Application.Empleados;

/// <summary>Write model for creating an employee.</summary>
public class CrearEmpleadoDto
{
    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono1 { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Puesto { get; set; }
    public string? Modalidad { get; set; }
    public string? JefeInmediato { get; set; }
    public bool Facturable { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public decimal? SalarioActual { get; set; }
    public string Moneda { get; set; } = "USD";
    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }
}

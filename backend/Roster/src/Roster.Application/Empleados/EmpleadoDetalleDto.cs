namespace Roster.Application.Empleados;

/// <summary>Read model for a single employee detail.</summary>
public class EmpleadoDetalleDto
{
    public long Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }

    // Personal
    public string? Direccion { get; set; }
    public string? Telefono1 { get; set; }
    public string? Telefono2 { get; set; }
    public DateOnly? FechaNacimiento { get; set; }

    // Labor
    public string? Puesto { get; set; }
    public string? Modalidad { get; set; }
    public string? JefeInmediato { get; set; }
    public bool Facturable { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public bool Activo { get; set; }

    // Compensation
    public decimal? SalarioActual { get; set; }
    public string Moneda { get; set; } = "USD";

    // Catalogs
    public string? Departamento { get; set; }
    public string? Pais { get; set; }
}

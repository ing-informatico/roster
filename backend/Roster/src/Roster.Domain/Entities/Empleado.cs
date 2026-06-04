namespace Roster.Domain.Entities;

/// <summary>Core employee entity.</summary>
public class Empleado
{
    public long Id { get; set; }

    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }

    // Personal data
    public string? Direccion { get; set; }
    public string? Telefono1 { get; set; }
    public string? Telefono2 { get; set; }
    public DateOnly? FechaNacimiento { get; set; }

    // Labor data
    public string? Puesto { get; set; }
    public string? Modalidad { get; set; }
    public string? JefeInmediato { get; set; }
    public bool Facturable { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public bool Activo { get; set; } = true;

    // Compensation (current value; history lives in separate tables)
    public decimal? SalarioActual { get; set; }
    public string Moneda { get; set; } = "USD";

    // Foreign keys
    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }

    // Navigation properties
    public Pais? Pais { get; set; }
    public Departamento? Departamento { get; set; }

    // History collections
    public ICollection<EmpleadoHistorialPuesto> HistorialPuestos { get; set; } = new List<EmpleadoHistorialPuesto>();
    public ICollection<EmpleadoHistorialSalario> HistorialSalarios { get; set; } = new List<EmpleadoHistorialSalario>();
    public ICollection<EmpleadoEvaluacion> Evaluaciones { get; set; } = new List<EmpleadoEvaluacion>();
    public ICollection<EmpleadoBono> Bonos { get; set; } = new List<EmpleadoBono>();
}

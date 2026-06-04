namespace Roster.Domain.Entities;

/// <summary>Position history entry for an employee.</summary>
public class EmpleadoHistorialPuesto
{
    public long Id { get; set; }
    public long EmpleadoId { get; set; }
    public string Puesto { get; set; } = string.Empty;
    public DateOnly Desde { get; set; }
    public DateOnly? Hasta { get; set; }
    public string? Motivo { get; set; }

    public Empleado? Empleado { get; set; }
}

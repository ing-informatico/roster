namespace Roster.Domain.Entities;

/// <summary>Salary history entry for an employee.</summary>
public class EmpleadoHistorialSalario
{
    public long Id { get; set; }
    public long EmpleadoId { get; set; }
    public decimal Salario { get; set; }
    public string Moneda { get; set; } = "USD";
    public DateOnly Desde { get; set; }
    public DateOnly? Hasta { get; set; }
    public string? Motivo { get; set; }

    public Empleado? Empleado { get; set; }
}

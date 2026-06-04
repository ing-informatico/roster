namespace Roster.Domain.Entities;

/// <summary>Bonus granted to an employee.</summary>
public class EmpleadoBono
{
    public long Id { get; set; }
    public long EmpleadoId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Moneda { get; set; } = "USD";
    public DateOnly OtorgadoEn { get; set; }

    public Empleado? Empleado { get; set; }
}

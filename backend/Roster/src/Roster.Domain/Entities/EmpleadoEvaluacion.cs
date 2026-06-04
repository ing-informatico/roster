namespace Roster.Domain.Entities;

/// <summary>Performance evaluation entry for an employee.</summary>
public class EmpleadoEvaluacion
{
    public long Id { get; set; }
    public long EmpleadoId { get; set; }
    public string Periodo { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public decimal EscalaMax { get; set; } = 5;
    public string? Comentario { get; set; }
    public DateOnly? EvaluadoEn { get; set; }

    public Empleado? Empleado { get; set; }
}

namespace Roster.Domain.Entities;

/// <summary>Country catalog (e.g. Colombia, Guatemala).</summary>
public class Pais
{
    public short Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

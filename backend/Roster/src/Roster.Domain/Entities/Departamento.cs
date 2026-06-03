namespace Roster.Domain.Entities;

/// <summary>Department catalog (e.g. ITS, Finanzas).</summary>
public class Departamento
{
    public short Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

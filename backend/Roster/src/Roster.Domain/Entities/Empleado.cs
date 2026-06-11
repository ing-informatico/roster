namespace Roster.Domain.Entities;

public class Empleado
{
    public long Id { get; set; }

    public int? NumeroFila { get; set; }
    public string? Genero { get; set; }
    public string? ModalidadCompensacion { get; set; }

    public string Codigo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Correo { get; set; }

    public string? Direccion { get; set; }
    public string? Telefono1 { get; set; }
    public DateOnly? FechaNacimiento { get; set; }

    public string? CentroCosto { get; set; }
    public string? IdCentroCosto { get; set; }
    public string? CentroCostoUbicacion { get; set; }
    public string? Proyecto { get; set; }

    public string? Puesto { get; set; }
    public string? Modalidad { get; set; }
    public string? Site { get; set; }
    public string? JefeInmediato { get; set; }
    public string? TeamLead { get; set; }
    public string? Sdm { get; set; }
    public string? Manager { get; set; }
    public bool Facturable { get; set; }
    public DateOnly? FechaIngreso { get; set; }
    public bool Activo { get; set; } = true;

    public string? IdObsPoliza { get; set; }
    public string? TipoSeguro { get; set; }
    public string? IdBeneficioHospAngeles { get; set; }

    public string? PadreOMadre { get; set; }
    public string? EquipoAsignado { get; set; }

    public decimal? SalarioActual { get; set; }
    public string Moneda { get; set; } = "USD";

    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }

    public Pais? Pais { get; set; }
    public Departamento? Departamento { get; set; }

    public ICollection<EmpleadoHistorialPuesto> HistorialPuestos { get; set; } = new List<EmpleadoHistorialPuesto>();
    public ICollection<EmpleadoHistorialSalario> HistorialSalarios { get; set; } = new List<EmpleadoHistorialSalario>();
    public ICollection<EmpleadoEvaluacion> Evaluaciones { get; set; } = new List<EmpleadoEvaluacion>();
    public ICollection<EmpleadoBono> Bonos { get; set; } = new List<EmpleadoBono>();
}

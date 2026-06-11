namespace Roster.Application.Empleados;

public class ActualizarEmpleadoDto
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string? Genero { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono1 { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? PadreOMadre { get; set; }

    public string? Puesto { get; set; }
    public string? Modalidad { get; set; }
    public string? ModalidadCompensacion { get; set; }
    public string? Site { get; set; }
    public string? JefeInmediato { get; set; }
    public string? TeamLead { get; set; }
    public string? Sdm { get; set; }
    public string? Manager { get; set; }
    public bool Facturable { get; set; }
    public DateOnly? FechaIngreso { get; set; }

    public string? CentroCosto { get; set; }
    public string? IdCentroCosto { get; set; }
    public string? CentroCostoUbicacion { get; set; }
    public string? Proyecto { get; set; }
    public string? EquipoAsignado { get; set; }

    public string? IdObsPoliza { get; set; }
    public string? TipoSeguro { get; set; }
    public string? IdBeneficioHospAngeles { get; set; }

    public decimal? SalarioActual { get; set; }
    public string Moneda { get; set; } = "USD";

    public short? PaisId { get; set; }
    public short? DepartamentoId { get; set; }
    public bool Activo { get; set; }
}

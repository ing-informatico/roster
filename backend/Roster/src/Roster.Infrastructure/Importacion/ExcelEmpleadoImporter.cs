using System.Globalization;
using ClosedXML.Excel;
using Roster.Application.Importacion;
using Roster.Domain.Entities;
using Roster.Infrastructure.Persistence;

namespace Roster.Infrastructure.Importacion;

public class ExcelEmpleadoImporter
{
    private readonly RosterDbContext _db;
    private static readonly CultureInfo Spanish = new("es-ES");

    public ExcelEmpleadoImporter(RosterDbContext db)
    {
        _db = db;
    }

    private const int ColNumero = 1;
    private const int ColGenero = 2;
    private const int ColModComp = 3;
    private const int ColPais = 4;
    private const int ColCodigo = 5;
    private const int ColNombre = 6;
    private const int ColDepartamento = 7;
    private const int ColCentroCosto = 8;
    private const int ColIdCentroCosto = 9;
    private const int ColCentroCostoUbic = 10;
    private const int ColProyecto = 11;
    private const int ColFacturacion = 12;
    private const int ColPuesto = 13;
    private const int ColJefe = 14;
    private const int ColIdObs = 15;
    private const int ColTipoSeguro = 16;
    private const int ColBeneficioHosp = 17;
    private const int ColTeamLead = 18;
    private const int ColSdm = 19;
    private const int ColManager = 20;
    private const int ColFechaIngreso = 21;
    private const int ColCorreo = 22;
    private const int ColMesCumple = 23;
    private const int ColDiaCumple = 24;
    private const int ColAnioCumple = 25;
    private const int ColTelefono = 26;
    private const int ColDireccion = 27;
    private const int ColPadreMadre = 28;
    private const int ColModalidad = 29;
    private const int ColSite = 30;
    private const int ColEquipo = 31;

    public async Task<ImportResultDto> ImportAsync(Stream excelStream)
    {
        var result = new ImportResultDto();

        using var workbook = new XLWorkbook(excelStream);
        var ws = workbook.Worksheets.First();

        var departamentos = _db.Departamentos.ToDictionary(d => d.Nombre, d => d, StringComparer.OrdinalIgnoreCase);
        var paises = _db.Paises.ToDictionary(p => p.Nombre, p => p, StringComparer.OrdinalIgnoreCase);
        var existentes = _db.Empleados.ToDictionary(e => e.Codigo, e => e);

        var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
        result.TotalFilas = Math.Max(0, lastRow - 1);

        for (int r = 2; r <= lastRow; r++)
        {
            var codigo = Clean(ws.Cell(r, ColCodigo).GetString());
            var nombre = Clean(ws.Cell(r, ColNombre).GetString());

            if (string.IsNullOrWhiteSpace(codigo) && string.IsNullOrWhiteSpace(nombre))
                continue;

            if (string.IsNullOrWhiteSpace(codigo))
            {
                result.Errores.Add(new ImportErrorDto { Fila = r, Motivo = "Codigo vacio." });
                result.Omitidos++;
                continue;
            }
            if (string.IsNullOrWhiteSpace(nombre))
            {
                result.Errores.Add(new ImportErrorDto { Fila = r, Motivo = "Nombre vacio." });
                result.Omitidos++;
                continue;
            }

            short? deptId = null;
            var deptNombre = Clean(ws.Cell(r, ColDepartamento).GetString());
            if (!string.IsNullOrWhiteSpace(deptNombre))
                deptId = ResolveDepartamento(deptNombre, departamentos);

            short? paisId = null;
            var paisNombre = Clean(ws.Cell(r, ColPais).GetString());
            if (!string.IsNullOrWhiteSpace(paisNombre))
                paisId = ResolvePais(paisNombre, paises);

            var empleado = existentes.TryGetValue(codigo, out var existing) ? existing : new Empleado();

            empleado.NumeroFila = ParseInt(Clean(ws.Cell(r, ColNumero).GetString()));
            empleado.Genero = Clean(ws.Cell(r, ColGenero).GetString());
            empleado.ModalidadCompensacion = Clean(ws.Cell(r, ColModComp).GetString());
            empleado.Codigo = codigo;
            empleado.NombreCompleto = nombre;
            empleado.Correo = Clean(ws.Cell(r, ColCorreo).GetString());
            empleado.Direccion = Clean(ws.Cell(r, ColDireccion).GetString());
            empleado.Telefono1 = Clean(ws.Cell(r, ColTelefono).GetString());
            empleado.CentroCosto = Clean(ws.Cell(r, ColCentroCosto).GetString());
            empleado.IdCentroCosto = Clean(ws.Cell(r, ColIdCentroCosto).GetString());
            empleado.CentroCostoUbicacion = Clean(ws.Cell(r, ColCentroCostoUbic).GetString());
            empleado.Proyecto = Clean(ws.Cell(r, ColProyecto).GetString());
            empleado.Puesto = Clean(ws.Cell(r, ColPuesto).GetString());
            empleado.Modalidad = Clean(ws.Cell(r, ColModalidad).GetString());
            empleado.Site = Clean(ws.Cell(r, ColSite).GetString());
            empleado.JefeInmediato = LimpiarPersona(Clean(ws.Cell(r, ColJefe).GetString()));
            empleado.TeamLead = LimpiarPersona(Clean(ws.Cell(r, ColTeamLead).GetString()));
            empleado.Sdm = LimpiarPersona(Clean(ws.Cell(r, ColSdm).GetString()));
            empleado.Manager = LimpiarPersona(Clean(ws.Cell(r, ColManager).GetString()));
            empleado.Facturable = EsBillable(Clean(ws.Cell(r, ColFacturacion).GetString()));
            empleado.IdObsPoliza = Clean(ws.Cell(r, ColIdObs).GetString());
            empleado.TipoSeguro = Clean(ws.Cell(r, ColTipoSeguro).GetString());
            empleado.IdBeneficioHospAngeles = Clean(ws.Cell(r, ColBeneficioHosp).GetString());
            empleado.PadreOMadre = Clean(ws.Cell(r, ColPadreMadre).GetString());
            empleado.EquipoAsignado = Clean(ws.Cell(r, ColEquipo).GetString());
            empleado.FechaIngreso = ParseFecha(ws.Cell(r, ColFechaIngreso));
            empleado.FechaNacimiento = ParseCumple(
                Clean(ws.Cell(r, ColMesCumple).GetString()),
                Clean(ws.Cell(r, ColDiaCumple).GetString()),
                Clean(ws.Cell(r, ColAnioCumple).GetString()));
            empleado.DepartamentoId = deptId;
            empleado.PaisId = paisId;
            empleado.Activo = true;
            empleado.Moneda = string.IsNullOrWhiteSpace(empleado.Moneda) ? "USD" : empleado.Moneda;

            if (existing is null)
            {
                _db.Empleados.Add(empleado);
                existentes[codigo] = empleado;
                result.Insertados++;
            }
            else
            {
                result.Actualizados++;
            }
        }

        await _db.SaveChangesAsync();
        return result;
    }

    private short ResolveDepartamento(string nombre, Dictionary<string, Departamento> cache)
    {
        if (cache.TryGetValue(nombre, out var d)) return d.Id;
        var nuevo = new Departamento { Nombre = nombre };
        _db.Departamentos.Add(nuevo);
        _db.SaveChanges();
        cache[nombre] = nuevo;
        return nuevo.Id;
    }

    private short ResolvePais(string nombre, Dictionary<string, Pais> cache)
    {
        if (cache.TryGetValue(nombre, out var p)) return p.Id;
        var nuevo = new Pais { Nombre = nombre };
        _db.Paises.Add(nuevo);
        _db.SaveChanges();
        cache[nombre] = nuevo;
        return nuevo.Id;
    }

    private static string? Clean(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var v = value.Trim();
        if (v == "#N/A" || v == "N/A" || v == "#N/D" || v == "-" || v == "—") return null;
        return v;
    }

    private static int? ParseInt(string? value) =>
        int.TryParse(value, out var n) ? n : null;

    private static bool EsBillable(string? value) =>
        value is not null && value.Contains("billable", StringComparison.OrdinalIgnoreCase);

    private static string? LimpiarPersona(string? value)
    {
        if (value is null) return null;
        var dash = value.LastIndexOf(" - ", StringComparison.Ordinal);
        return dash > 0 ? value[..dash].Trim() : value;
    }

    private DateOnly? ParseFecha(IXLCell cell)
    {
        if (cell.DataType == XLDataType.DateTime && cell.TryGetValue<DateTime>(out var dt))
            return DateOnly.FromDateTime(dt);

        var text = Clean(cell.GetString());
        if (text is null) return null;

        if (DateTime.TryParse(text, Spanish, DateTimeStyles.None, out var parsedEs))
            return DateOnly.FromDateTime(parsedEs);

        if (DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedInv))
            return DateOnly.FromDateTime(parsedInv);

        string[] formats = { "d 'de' MMMM 'de' yyyy", "dd 'de' MMMM 'de' yyyy" };
        if (DateTime.TryParseExact(text, formats, Spanish, DateTimeStyles.None, out var exact))
            return DateOnly.FromDateTime(exact);

        return null;
    }

    private static DateOnly? ParseCumple(string? mesTexto, string? diaTexto, string? anioTexto)
    {
        if (mesTexto is null || diaTexto is null || anioTexto is null) return null;
        var mes = MesDesdeTexto(mesTexto);
        if (mes == 0) return null;
        if (!int.TryParse(diaTexto, out var dia)) return null;
        if (!int.TryParse(anioTexto, out var anio)) return null;
        return SafeDate(anio, mes, dia);
    }

    private static int MesDesdeTexto(string texto)
    {
        for (int m = 1; m <= 12; m++)
        {
            var nombre = Spanish.DateTimeFormat.GetMonthName(m);
            if (string.Equals(nombre, texto, StringComparison.OrdinalIgnoreCase))
                return m;
        }
        return 0;
    }

    private static DateOnly? SafeDate(int year, int month, int day)
    {
        if (year < 1900 || year > 2100 || month < 1 || month > 12 || day < 1 || day > 31)
            return null;
        try { return new DateOnly(year, month, day); }
        catch { return null; }
    }
}

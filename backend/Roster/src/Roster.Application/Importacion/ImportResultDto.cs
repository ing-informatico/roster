namespace Roster.Application.Importacion;

/// <summary>Result of an Excel import operation.</summary>
public class ImportResultDto
{
    public int TotalFilas { get; set; }
    public int Insertados { get; set; }
    public int Actualizados { get; set; }
    public int Omitidos { get; set; }
    public List<ImportErrorDto> Errores { get; set; } = new();
}

/// <summary>A single row that could not be imported.</summary>
public class ImportErrorDto
{
    public int Fila { get; set; }
    public string Motivo { get; set; } = string.Empty;
}

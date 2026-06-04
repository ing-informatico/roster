namespace Roster.Application.Catalogos;

/// <summary>Generic read model for simple catalogs (id + name).</summary>
public class CatalogoItemDto
{
    public short Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

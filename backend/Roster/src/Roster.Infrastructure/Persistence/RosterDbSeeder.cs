using Microsoft.EntityFrameworkCore;
using Roster.Domain.Entities;

namespace Roster.Infrastructure.Persistence;

/// <summary>Seeds base catalogs idempotently (only when tables are empty).</summary>
public static class RosterDbSeeder
{
    public static async Task SeedAsync(RosterDbContext db, CancellationToken cancellationToken = default)
    {
        if (!await db.Departamentos.AnyAsync(cancellationToken))
        {
            var departamentos = new[] { "ITS", "Finanzas", "RRHH", "Operaciones", "Ventas", "Calidad" }
                .Select(nombre => new Departamento { Nombre = nombre });
            db.Departamentos.AddRange(departamentos);
        }

        if (!await db.Paises.AnyAsync(cancellationToken))
        {
            var paises = new[] { "Colombia", "Guatemala", "Mexico", "El Salvador" }
                .Select(nombre => new Pais { Nombre = nombre });
            db.Paises.AddRange(paises);
        }

        await db.SaveChangesAsync(cancellationToken);
    }
}

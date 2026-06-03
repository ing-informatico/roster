using Microsoft.EntityFrameworkCore;
using Roster.Domain.Entities;

namespace Roster.Infrastructure.Persistence;

/// <summary>EF Core database context for the Roster schema.</summary>
public class RosterDbContext : DbContext
{
    public RosterDbContext(DbContextOptions<RosterDbContext> options)
        : base(options)
    {
    }

    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Pais> Paises => Set<Pais>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("roster");

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.ToTable("pais");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nombre).HasMaxLength(80).IsRequired();
            entity.HasIndex(p => p.Nombre).IsUnique();
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.ToTable("departamento");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Nombre).HasMaxLength(80).IsRequired();
            entity.HasIndex(d => d.Nombre).IsUnique();
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("empleado");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Codigo).HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.Codigo).IsUnique();

            entity.Property(e => e.NombreCompleto).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.HasIndex(e => e.Correo).IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(e => e.Pais)
                  .WithMany(p => p.Empleados)
                  .HasForeignKey(e => e.PaisId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Departamento)
                  .WithMany(d => d.Empleados)
                  .HasForeignKey(e => e.DepartamentoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

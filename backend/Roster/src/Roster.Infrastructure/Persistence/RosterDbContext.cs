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
    public DbSet<EmpleadoHistorialPuesto> HistorialPuestos => Set<EmpleadoHistorialPuesto>();
    public DbSet<EmpleadoHistorialSalario> HistorialSalarios => Set<EmpleadoHistorialSalario>();
    public DbSet<EmpleadoEvaluacion> Evaluaciones => Set<EmpleadoEvaluacion>();
    public DbSet<EmpleadoBono> Bonos => Set<EmpleadoBono>();

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

            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Telefono1).HasMaxLength(40);
            entity.Property(e => e.Telefono2).HasMaxLength(40);
            entity.Property(e => e.Puesto).HasMaxLength(120);
            entity.Property(e => e.Modalidad).HasMaxLength(40);
            entity.Property(e => e.JefeInmediato).HasMaxLength(150);
            entity.Property(e => e.SalarioActual).HasColumnType("numeric(12,2)");
            entity.Property(e => e.Moneda).HasMaxLength(3).HasDefaultValue("USD");
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

        modelBuilder.Entity<EmpleadoHistorialPuesto>(entity =>
        {
            entity.ToTable("empleado_historial_puesto");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Puesto).HasMaxLength(120).IsRequired();
            entity.Property(h => h.Motivo).HasMaxLength(120);
            entity.HasOne(h => h.Empleado)
                  .WithMany(e => e.HistorialPuestos)
                  .HasForeignKey(h => h.EmpleadoId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(h => h.EmpleadoId);
        });

        modelBuilder.Entity<EmpleadoHistorialSalario>(entity =>
        {
            entity.ToTable("empleado_historial_salario");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Salario).HasColumnType("numeric(12,2)");
            entity.Property(h => h.Moneda).HasMaxLength(3).HasDefaultValue("USD");
            entity.Property(h => h.Motivo).HasMaxLength(120);
            entity.HasOne(h => h.Empleado)
                  .WithMany(e => e.HistorialSalarios)
                  .HasForeignKey(h => h.EmpleadoId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(h => h.EmpleadoId);
        });

        modelBuilder.Entity<EmpleadoEvaluacion>(entity =>
        {
            entity.ToTable("empleado_evaluacion");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Periodo).HasMaxLength(20).IsRequired();
            entity.Property(h => h.Rate).HasColumnType("numeric(3,1)");
            entity.Property(h => h.EscalaMax).HasColumnType("numeric(3,1)").HasDefaultValue(5);
            entity.Property(h => h.Comentario).HasMaxLength(300);
            entity.HasOne(h => h.Empleado)
                  .WithMany(e => e.Evaluaciones)
                  .HasForeignKey(h => h.EmpleadoId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(h => h.EmpleadoId);
        });

        modelBuilder.Entity<EmpleadoBono>(entity =>
        {
            entity.ToTable("empleado_bono");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Tipo).HasMaxLength(60).IsRequired();
            entity.Property(h => h.Monto).HasColumnType("numeric(12,2)");
            entity.Property(h => h.Moneda).HasMaxLength(3).HasDefaultValue("USD");
            entity.HasOne(h => h.Empleado)
                  .WithMany(e => e.Bonos)
                  .HasForeignKey(h => h.EmpleadoId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(h => h.EmpleadoId);
        });
    }
}

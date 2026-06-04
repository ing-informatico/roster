using Microsoft.EntityFrameworkCore;
using Roster.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<RosterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Apply pending EF Core migrations and seed base catalogs on startup (idempotent).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RosterDbContext>();
    db.Database.Migrate();
    await RosterDbSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Health check endpoint: confirms the API is alive.
app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    service = "Roster.Api",
    timestamp = DateTime.UtcNow
}))
.WithName("HealthCheck");

app.MapControllers();

app.Run();

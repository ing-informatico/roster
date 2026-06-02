var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

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

app.Run();

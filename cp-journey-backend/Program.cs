using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddSingleton<ModelConverter>();

builder.Configuration.AddUserSecrets<Program>();

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "Database");

Directory.CreateDirectory(databasePath);

var connectionString = $"Data Source={Path.Combine(databasePath, "app.db")}";

builder.Services.AddDbContext<AppDbContext>(options 
    => options.UseSqlite(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply the remaining migrations before starting the application
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
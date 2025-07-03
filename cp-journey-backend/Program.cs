using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IProblemService, ProblemService>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IProblemRepository, ProblemRepository>();

builder.Services.AddSingleton<ModelConverter>();

builder.Configuration.AddUserSecrets<Program>();

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "Database");

Directory.CreateDirectory(databasePath);

var connectionString = $"Data Source={Path.Combine(databasePath, "app.db")}";

builder.Services.AddDbContext<AppDbContext>(options 
    => options.UseSqlite(connectionString));

// TODO: improve this policy before putting it in production
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(); // need to be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

// Apply the remaining migrations before starting the application
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

await app.RunAsync();
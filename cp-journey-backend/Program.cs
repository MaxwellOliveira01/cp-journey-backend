using cp_journey_backend.Persistence;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IProfileService, ProfileService>();

// refatorar isso aqui? criar um appDbContext unico
// e nele eu injeto o InMemoryDb ou o normal
// mas dai nao preciso de duas implementações pra cada repositorio
builder.Services.AddScoped<IProfileRepository, InMemoryProfileRepository>();
builder.Services.AddScoped<IUniversityRepository, InMemoryUniversityRepository>();

builder.Services.AddSingleton<ModelConverter>();
builder.Services.AddSingleton<InMemoryDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
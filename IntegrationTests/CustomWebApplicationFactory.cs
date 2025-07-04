using cp_journey_backend.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests {
    public class CustomWebApplicationFactory : WebApplicationFactory<Program> {

        protected override void ConfigureWebHost(IWebHostBuilder builder) {

            builder.ConfigureServices((context, services) => {

                var dbContextOptionsDescriptors = services
                    .Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>))
                    .ToList();

                foreach (var descriptor in dbContextOptionsDescriptors)
                    services.Remove(descriptor);

                var appDbContextDescriptors = services
                    .Where(d => d.ServiceType == typeof(AppDbContext))
                    .ToList();

                foreach (var descriptor in appDbContextDescriptors)
                    services.Remove(descriptor);

                var config = context.Configuration;
                
                var connectionString = config.GetConnectionString("TestDatabase")
                    ?? throw new InvalidOperationException("Connection string 'TestDatabase' not found in configuration.");
                
                services.AddDbContext<AppDbContext>(options => 
                    options.UseNpgsql(connectionString)
                );

                using (var scope = services.BuildServiceProvider().CreateScope()) {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }
                
            });
        }

    }
}

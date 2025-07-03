using cp_journey_backend.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace IntegrationTests {
    public class CustomWebApplicationFactory : WebApplicationFactory<Program> {

        protected override void ConfigureWebHost(IWebHostBuilder builder) {

            builder.ConfigureServices(services => {

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

                services.AddSingleton<DbConnection>(container => {
                    var connection = new SqliteConnection("Filename=:memory:");
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<AppDbContext>((container, options)=> {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });

            });
        }

    }
}

using cp_journey_backend.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests {
    public class CustomWebApplicationFactory : WebApplicationFactory<Program> {

        protected override void ConfigureWebHost(IWebHostBuilder builder) {

            builder.ConfigureServices(services => {

                var dbContextOptionsDescriptors = services
                    .Where(d => d.ServiceType == typeof(DbContextOptions))
                    .ToList();

                foreach (var descriptor in dbContextOptionsDescriptors)
                    services.Remove(descriptor);

                var appDbContextDescriptors = services
                    .Where(d => d.ServiceType == typeof(AppDbContext))
                    .ToList();

                foreach (var descriptor in appDbContextDescriptors)
                    services.Remove(descriptor);


                services.AddDbContext<AppDbContext>(options => {
                    options.UseInMemoryDatabase("TestDb");
                });

            });
        }

    }
}

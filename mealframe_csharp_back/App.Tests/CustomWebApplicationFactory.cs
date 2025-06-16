using App.DAL.EF;
using App.DAL.EF.DataSeeding;
using App.Domain.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace App.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            
            var connectionString = "Host=localhost;Port=5432;Database=TEST;Username=postgres;Password=postgres";
            services.AddDbContext<AppDbContext>(options =>
                options
                    .UseNpgsql(
                        connectionString,
                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    )
                    .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    // disable tracking, allow id based shared entity creation
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
            );
            
            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.Migrate();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            using var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
            
            try
            {
                AppDataInit.SeedIdentity(userManager);
                AppDataInit.SeedAppData(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database with test messages. " +
                                    "Error: {Message}", ex.Message);
            }
        });
    }
}

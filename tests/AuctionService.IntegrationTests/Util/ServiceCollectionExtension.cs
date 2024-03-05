using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests.Util
{
    public static class ServiceCollectionExtension
    {
        public static void RemoveDbContext<T>(this IServiceCollection service)
        {
            var descriptor = service.SingleOrDefault(d => 
            d.ServiceType == typeof(DbContextOptions<AuctionDbContext>));

            if(descriptor != null) service.Remove(descriptor);
        }

        public static void EnsureCreated<T>(this IServiceCollection service)
        {
            var sp = service.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedService = scope.ServiceProvider;
            var db = scopedService.GetRequiredService<AuctionDbContext>();

            db.Database.Migrate();
            DbHelper.InitDbForTests(db);
        }
    }
}
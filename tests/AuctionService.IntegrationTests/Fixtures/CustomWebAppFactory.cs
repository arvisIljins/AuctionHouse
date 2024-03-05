using AuctionService.Data;
using AuctionService.IntegrationTests.Util;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using WebMotions.Fake.Authentication.JwtBearer;

namespace AuctionService.IntegrationTests.Fixtures
{
    public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder().Build();
        public async Task InitializeAsync()
        {
            await _postgresSqlContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(service => 
            {
                service.RemoveDbContext<AuctionDbContext>();

                service.AddDbContext<AuctionDbContext>(options => 
                {
                    options.UseNpgsql(_postgresSqlContainer.GetConnectionString());
                });

                service.AddMassTransitTestHarness();

                service.EnsureCreated<AuctionDbContext>();
                service.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer(opt =>
                    {
                        opt.BearerValueType = FakeJwtBearerBearerValueType.Jwt;
                    }
                );
            });
        }

        Task IAsyncLifetime.DisposeAsync() => _postgresSqlContainer.DisposeAsync().AsTask();
    }
}
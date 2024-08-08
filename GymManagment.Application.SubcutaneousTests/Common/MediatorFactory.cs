using GymManagement.Api;
using GymManagement.Infrastructure.Common.Presistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.SubcutaneousTests.Common
{
    public class MediatorFactory : WebApplicationFactory<IAssemblyMarker>
    {
        private SqliteTestDatabase _testDatabase = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _testDatabase = SqliteTestDatabase.CreateAndInitialize();

            builder.ConfigureTestServices(services =>
            {
                services
                .RemoveAll<DbContextOptions<GymManagementDbContext>>()
                .AddDbContext<GymManagementDbContext>((sp, options) => options.UseSqlite(_testDatabase.Connection));

            });
        }

        public IMediator CreateMediator()
        {
            var serviceScope = Services.CreateScope();

            _testDatabase.ResetDatabase(); //for each test to run with a consistent and clean database environment

            return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public new Task DisposeAsync()
        {
            _testDatabase.Dispose();

            return Task.CompletedTask;
        }

    }
}

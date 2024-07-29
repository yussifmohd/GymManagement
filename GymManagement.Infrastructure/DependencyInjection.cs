using GymManagement.Infrastructure.Admins.Persistence;
using GymManagment.Application.Common.Interfaces;
using GymManagment.Infrastructure.Common.Presistence;
using GymManagment.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<GymManagmentDbContext>(options => options.UseSqlite("Data Source = GymManagement.db"));

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagmentDbContext>());

            return services;
        }

    }
}

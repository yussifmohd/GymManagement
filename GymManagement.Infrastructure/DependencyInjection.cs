using GymManagement.Infrastructure.Admins.Persistence;
using GymManagement.Infrastructure.Gyms.Presistance;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Common.Presistence;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<GymManagementDbContext>(options => options.UseSqlite("Data Source = GymManagement.db"));

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IAdminRepository, AdminsRepository>();
            services.AddScoped<IGymRepository, GymRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());

            return services;
        }

    }
}

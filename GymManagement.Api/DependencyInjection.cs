﻿namespace GymManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddProblemDetails();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}

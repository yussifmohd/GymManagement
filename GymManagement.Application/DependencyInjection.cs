using FluentValidation;
using GymManagement.Application.Common.Behavior;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                options.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Add The Validation Behavior to the pipeline before handling the request
            });


            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection)); //Get The various Validators of each request
            return services;
        }
    }
}

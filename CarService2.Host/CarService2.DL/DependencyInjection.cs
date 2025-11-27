using CarService3.DL.Interfaces;
using CarService3.DL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarService3.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            // Register DL services here
            services.AddSingleton<ICustomerRepository, CustomerStaticRepository>();
            return services;
        }
    }
}

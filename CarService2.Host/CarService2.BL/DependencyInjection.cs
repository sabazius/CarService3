using CarService3.BL.Interfaces;
using CarService3.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService3.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Register BL services here
            services.AddSingleton<ICustomerService, CustomerService>();
            return services;
        }
    }
}

using CarService3.DL.Interfaces;
using CarService3.DL.Repositories;
using CarService3.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace CarService3.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddDataLayer(this IServiceCollection services,
                IConfiguration config)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            //Add configurations here
            services.Configure<MongoDbConfiguration>(
                config.GetSection(
                    nameof(MongoDbConfiguration)));

            // Register DL services here
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            //services.AddSingleton<ICarRepository, CarStaticRepository>();
            return services;
        }
    }
}

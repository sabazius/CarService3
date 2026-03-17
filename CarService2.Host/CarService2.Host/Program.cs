using CarService3.BL;
using CarService3.DL;
using CarService3.DL.Interfaces;
using CarService3.Host.Validators;
using CarService3.Models.Entities;
using FluentValidation;
using Mapster;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace CarService3.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mc = new Car()
            {
                Id = Guid.NewGuid(),
                Model = "hoge",
                Year = 2020,
                BasePrice = 1000000
            };
           
            // Call Serialize/Deserialize, that's all.
            byte[] bytes = MessagePackSerializer.Serialize(mc);
            Car mc2 = MessagePackSerializer.Deserialize<Car>(bytes);

            // You can dump MessagePack binary blobs to human readable json.
            // Using indexed keys (as opposed to string keys) will serialize to MessagePack arrays,
            // hence property names are not available.
            // [99,"hoge","huga"]
            var json = MessagePackSerializer.ConvertToJson(bytes);
            Console.WriteLine(json);

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services
                .AddValidatorsFromAssemblyContaining<AddCustomerValidator>();

            builder.Services
                .AddDataLayer(builder.Configuration)
                .AddBusinessLogicLayer();

            builder.Services.AddMapster();

            builder.Services.AddControllers();
         
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Service 2", Version = "v1" });
            });

            builder.Services.AddHealthChecks();

            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.MapHealthChecks("/health");

            app.UseAuthorization();

            app.MapControllers();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Car Service 2 V1");
            });

            app.UseSwagger();

            app.Run();
        }
    }
}

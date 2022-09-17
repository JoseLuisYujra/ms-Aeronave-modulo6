using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using Aeronaves.WebApi.ManejadorRabbit;
using static MassTransit.MessageHeaders;

namespace Aeronaves.WebApi.Persistencia
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           IConfiguration configuration)
        {
            AddRabbitMq(services, configuration);

            return services;
        }


        private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqHost = configuration["RabbitMq:Host"];
            var rabbitMqPort = configuration["RabbitMq:Port"];
            var rabbitMqUserName = configuration["RabbitMq:UserName"];
            var rabbitMqPassword = configuration["RabbitMq:Password"];

            
            services.AddMassTransit(config =>
            {
                config.AddConsumer<AeronaveCreadaConsumer>().Endpoint(endpoint => endpoint.Name = AeronaveCreadaConsumer.QueueName);
                //config.AddConsumer<AeronaveCreadaConsumer>();
                config.UsingRabbitMq((context, cfg) =>
                {
                    var uri = string.Format("amqp://{0}:{1}@{2}:{3}", rabbitMqUserName, rabbitMqPassword, rabbitMqHost, rabbitMqPort);
                    cfg.Host(uri);

                    cfg.ConfigureEndpoints(context);
                    
                    /*
                    cfg.ReceiveEndpoint(AeronaveCreadaConsumer.QueueName, endpoint =>
                    {
                        endpoint.ConfigureConsumer<AeronaveCreadaConsumer>(context);
                    });
                    */
                });
            });


          
        }


    }
}

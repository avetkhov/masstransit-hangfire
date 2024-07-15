using MassTransit;
using Microsoft.Extensions.Options;

namespace AV.MasstransitHangfire.CentralServer.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddOptions();
        services.AddMessageBrokers();

        return services;
    }

    private static void AddOptions(this IServiceCollection services)
    {
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(RabbitMqTransportOptions)).Bind(settings);
            });
    }

    private static void AddMessageBrokers(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(Program).Assembly);
            
            x.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqTransportOptions>>().Value;
                
                cfg.Host(options.Host, configurator =>
                {
                    configurator.Username(options.User);
                    configurator.Password(options.Pass);
                });

                cfg.UseNewtonsoftJsonSerializer();
                cfg.UseNewtonsoftJsonDeserializer();
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
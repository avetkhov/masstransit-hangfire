using AV.MasstransitHangfire.WorkerServer.Api.Options;
using Hangfire;
using Hangfire.MemoryStorage;
using MassTransit;
using Microsoft.Extensions.Options;

namespace AV.MasstransitHangfire.WorkerServer.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddOptions();
        services.AddSchedulers();
        services.AddMessageBrokers();

        return services;
    }

    private static void AddOptions(this IServiceCollection services)
    {
        services.AddOptions<KeepAliveWorkerOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(KeepAliveWorkerOptions)).Bind(settings);
            })
            .ValidateDataAnnotations();
        
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(RabbitMqTransportOptions)).Bind(settings);
            });
        
        services.AddOptions<MassTransitHostOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(MassTransitHostOptions)).Bind(settings);
            });
    }

    private static void AddSchedulers(this IServiceCollection services)
    {
        services.AddHangfireServer();
        services.AddHangfire(configuration =>
        {
            configuration.UseRecommendedSerializerSettings();
            configuration.UseMemoryStorage();
        });
    }

    private static void AddMessageBrokers(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddPublishMessageScheduler();
            x.AddHangfireConsumers();

            x.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqTransportOptions>>().Value;
                
                cfg.Host(options.Host, configurator =>
                {
                    configurator.Username(options.User);
                    configurator.Password(options.Pass);
                });

                cfg.UsePublishMessageScheduler();
                cfg.UseNewtonsoftJsonSerializer();
                cfg.UseNewtonsoftJsonDeserializer();
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
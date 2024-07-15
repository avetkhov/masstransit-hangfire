using AV.MasstransitHangfire.Common.Enums;
using AV.MasstransitHangfire.Models.Models;
using AV.MasstransitHangfire.WorkerServer.Api.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace AV.MasstransitHangfire.WorkerServer.Api.Workers;

public class KeepAliveWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly KeepAliveWorkerOptions _options;
    private readonly ILogger<KeepAliveWorker> _logger;

    public KeepAliveWorker(IServiceProvider serviceProvider,
        IOptions<KeepAliveWorkerOptions> options, ILogger<KeepAliveWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<IMessageScheduler>();
            
            await scheduler.SchedulePublish<KeepAliveWorkerModel>(DateTime.UtcNow.AddSeconds(_options.TimeoutInSec),
                new KeepAliveWorkerModel
                {
                    Id = Guid.NewGuid(),
                    Status = WorkerStatus.Running
                }, stoppingToken);
        
            _logger.LogInformation("Keepalive sending at: {Time}", DateTimeOffset.Now);
        }
    }
}
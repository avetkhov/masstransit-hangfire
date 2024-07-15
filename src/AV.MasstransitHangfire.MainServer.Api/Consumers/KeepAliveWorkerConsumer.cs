using AV.MasstransitHangfire.Models.Models;
using MassTransit;

namespace AV.MasstransitHangfire.CentralServer.Api.Consumers;

public class KeepAliveWorkerConsumer : IConsumer<KeepAliveWorkerModel>
{
    private readonly ILogger<KeepAliveWorkerConsumer> _logger;

    public KeepAliveWorkerConsumer(ILogger<KeepAliveWorkerConsumer> logger)
    {
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<KeepAliveWorkerModel> context)
    {
        _logger.LogInformation("Worker status: {Status} ({Id}) received at {Time}",
            context.Message.Status,
            context.Message.Id,
            DateTime.Now);
        
        await Task.CompletedTask;
    }
}
using AV.MasstransitHangfire.WorkerServer.Api.Extensions;
using AV.MasstransitHangfire.WorkerServer.Api.Workers;
using Hangfire;

namespace AV.MasstransitHangfire.WorkerServer.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.RegisterServices();
        services.AddHostedService<KeepAliveWorker>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseDeveloperExceptionPage()
            .UseHttpsRedirection()
            .UseRouting()
            .UseHangfireDashboard();
    }
}
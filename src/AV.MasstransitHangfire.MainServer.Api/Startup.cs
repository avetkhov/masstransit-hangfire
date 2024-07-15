using AV.MasstransitHangfire.CentralServer.Api.Extensions;

namespace AV.MasstransitHangfire.CentralServer.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.RegisterServices();
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
            .UseRouting();
    }
}
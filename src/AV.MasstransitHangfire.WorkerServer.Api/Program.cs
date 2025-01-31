namespace AV.MasstransitHangfire.WorkerServer.Api;

public static class Program
{
    public static async Task Main(string[] args) =>
        await CreateHostBuilder(args).Build().RunAsync();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); });
}
namespace WebApi;
using Microsoft.Extensions.Logging;
using Serilog;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var log = Log.Logger = new LoggerConfiguration().WriteTo.Console()
                                                        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                                                        .CreateLogger();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<MyDbContext>();
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddSerilog(log);
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();
        app.Run();
    }
}








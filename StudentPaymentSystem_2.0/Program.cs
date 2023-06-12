using Serilog.Events;
using Serilog;
using StudentPaymentSystem.Infrustructure;
using Serilog.Sinks.TelegramBot;
using StudentPaymentSystem.Application;
using StudentPaymentSystem_2._0.Logging;
using StudentPaymentSystem_2._0;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
       LoggingConfigurations.UseLogging(configuration);
        ConfigurationServices.AddRateLimiters(builder);
        // Add services to the container.
        builder.Host.UseSerilog();
        builder.Services.AddControllers();
        builder.Services.AddInfrastructureService(builder.Configuration);
        builder.Services.AddApplicationService();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRateLimiter();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
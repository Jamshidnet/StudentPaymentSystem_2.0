using Serilog.Events;
using Serilog;
using StudentPaymentSystem.Infrustructure;
using Serilog.Sinks.TelegramBot;
using StudentPaymentSystem.Application;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .WriteTo.TelegramBot(
                            token: configuration["TelegramBot:Token"],
                            chatId: configuration["TelegramBot:ChatId"],
                            restrictedToMinimumLevel: LogEventLevel.Information
                        )
                        .Enrich.FromLogContext()
                        .CreateLogger();
        // Add services to the container.

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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
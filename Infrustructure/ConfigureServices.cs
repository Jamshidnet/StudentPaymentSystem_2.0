using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentPaymentSystem.Application.Common.Interfaces;
using StudentPaymentSystem.Infrustructure.Persistence;
using StudentPaymentSystem.Infrustructure.Persistence.Interceptors;
using StudentPaymentSystem.Infrustructure.Services;

namespace StudentPaymentSystem.Infrustructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnection"));
        });

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IGuidGenerator, GuidGeneratorService>();
        return services;
    }
}
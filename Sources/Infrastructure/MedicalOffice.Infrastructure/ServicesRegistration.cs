using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using MedicalOffice.Infrastructure.Crypto;
using MedicalOffice.Infrastructure.Log;
using MedicalOffice.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalOffice.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.Configure<LoggerSettings>(configuration.GetSection("LoggerSettings"));
        services.AddTransient<ILogger, Logger>();

        services.AddCryptography(configuration);
        
        return services;
    }
}
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Infrastructure.Crypto;
using MedicalOffice.Infrastructure.Log;
using MedicalOffice.Infrastructure.Mail;
using MedicalOffice.Infrastructure.Token;
using MedicalOffice.Infrastructure.Totp;
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

        services.AddTransient<ITotpHandler, TotpHandler>();

        services.AddTokenGenerator(configuration);
        services.AddCryptography(configuration);
        
        return services;
    }
}
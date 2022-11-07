using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.WebApi.Crypto;
using MedicalOffice.WebApi.Log;
using MedicalOffice.WebApi.Mail;
using MedicalOffice.WebApi.Token;
using MedicalOffice.WebApi.Totp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalOffice.WebApi;

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
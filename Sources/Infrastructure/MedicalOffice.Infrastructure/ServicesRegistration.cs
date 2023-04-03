using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models.EmailSetting;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Models.Sms;
using MedicalOffice.Infrastructure.FetchData;
using MedicalOffice.Infrastructure.Sms;
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

        services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));
        services.AddTransient<ISmsSender, SmsSender>();

        services.AddTransient<ITotpHandler, TotpHandler>();

        services.AddTokenGenerator(configuration);
        services.AddCryptography(configuration);
        services.AddFetchData(configuration);

        services.AddTransient<IUserResolverService, UserResolverService>();
        services.AddTransient<IContextResolver, ContextResolverService>();
        
        services.AddHttpContextAccessor();

        return services;
    }
}
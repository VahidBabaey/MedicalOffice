using FluentValidation;
using MediatR;
using MedicalOffice.Application.Dtos.Identity.Validators;
using MedicalOffice.Application.Dtos.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MedicalOffice.Application.CommonValidations;

namespace MedicalOffice.Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ICommonValidators,CommonValidators>();

        services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserValidator>();

        return services;
    }
}
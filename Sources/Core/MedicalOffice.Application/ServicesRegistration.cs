using FluentValidation;
using MediatR;
using MedicalOffice.Application.Dtos.Identity.Validators;
using MedicalOffice.Application.Dtos.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Dtos.OfficeDTO.Validators;

namespace MedicalOffice.Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());


        #region Identity
        services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserValidator>();
        services.AddScoped<IValidator<AuthenticateByPasswordDTO>, AuthenticateByPasswordValidator>();
        services.AddScoped<IValidator<AuthenticateByTotpDTO>, AuthenticateByTotpValidator>();
        services.AddScoped<IValidator<PhoneNumberDTO>, PhoneNumberValidator>();
        services.AddScoped<IValidator<SetPasswordDTO>, SetPasswordValidator>();
        services.AddScoped<IValidator<ResetPasswordDTO>, ResetPasswordValidator>();
        services.AddScoped<IValidator<UserRoleDTO>, UserRoleValidator>();
        #endregion Identity

        #region Office
        services.AddScoped<IValidator<OfficeDTO>, OfficeValidator>();
        #endregion Office

        return services;
    }
}
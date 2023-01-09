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
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO.Validators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Validator;
using MedicalOffice.WebApi.WebApi.Controllers;

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
        services.AddScoped<IValidator<GetByPhoneNumberDTO>, GetByPhoneNumberValidator>();
        services.AddScoped<IValidator<SetPasswordDTO>, SetPasswordValidator>();
        services.AddScoped<IValidator<ResetPasswordDTO>, ResetPasswordValidator>();
        services.AddScoped<IValidator<UpdateUserRoleDTO>, UpdateUserRoleValidator>();
        #endregion Identity

        #region Office
        services.AddScoped<IValidator<OfficeDTO>, OfficeValidator>();
        #endregion Office

        #region ServiceDuration
        services.AddScoped<IValidator<ServiceDurationDTO>, ServiceDurationValidator>();
        #endregion

        #region MedicalStaffSchedule
        services.AddScoped<IValidator<MedicalStaffScheduleDTO>, MedicalStaffScheduleValidator>();
        services.AddScoped<IValidator<MedicalStaffDaySchedule>, MedicalStaffDayScheduleValidator>();
        #endregion

        #region Appointment
        services.AddScoped<IValidator<AddAppointmentDto>, AddAppointmentValidator>();
        services.AddScoped<IValidator<SearchAppointmentsDTO>, SearchAppointmentValidator>();
        services.AddScoped<IValidator<FilterFields>, FilterFieldsValidator>();
        services.AddScoped<IValidator<GetSpecificPeriodAppointmentDTO>, GetSpecificPeriodAppointmentValidator>();
        services.AddScoped<IValidator<UpdateAppointmentDescriptionDTO>, UpdateAppointmentDescriptionValidator>();
        services.AddScoped<IValidator<UpdateAppointmentTypeDTO>, AppointmentTypeValidator>();
        services.AddScoped<IValidator<UpdateAppointmentDTO>, TransferAppointmentValidator>();
        services.AddScoped<IValidator<UpdateAppointmentPatientInfoDto>, UpdateAppointmnetPatientInfoValidator>();
        #endregion

        return services;
    }
}
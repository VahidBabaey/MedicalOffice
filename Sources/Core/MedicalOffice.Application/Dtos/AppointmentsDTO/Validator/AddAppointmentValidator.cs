using FluentValidation;
using FluentValidation.Results;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons.validators;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceIdValidator = MedicalOffice.Application.Dtos.Common.CommonValidators.ServiceIdValidator;
#nullable disable

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AddAppointmentValidator : AbstractValidator<AddAppointmentDto>
    {
        private readonly IOfficeResolver _officeResolver;

        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private static readonly int minimumLength = 3;
        public AddAppointmentValidator(
            IOfficeResolver officeResolver,
            IServiceRepository serviceRepository,
            IMedicalStaffRepository medicalStaffRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _medicalStaffRepository = medicalStaffRepository;

            var validAppointmentTypeForNullTime = new AppointmentType[] { AppointmentType.waiting };
            var invalidAppointmentTypes = new AppointmentType[] { AppointmentType.FinalApproval, AppointmentType.Canceled };

            Include(new IPhoneNumberValidator());
            Include(new INationalIdValidator());
            //Include(new ServiceIdValidator(_serviceRepository, _officeResolver));
            Include(new MedicalStaffValidator(_medicalStaffRepository,_officeResolver));

            RuleFor(x => x.AppointmentType)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required")
                .Must(x => !invalidAppointmentTypes.Contains(x))
                    .WithMessage("FinalApproval and Canceled are not valid types during adding appointment process");

            RuleFor(x => x.PatientName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("PatientName"))
                .MinimumLength(minimumLength)
                    .WithMessage(ValidationMessage.MinimumLength.For("PatientName", minimumLength));

            RuleFor(x => x.PatientLastName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("PatientLastName"));

            RuleFor(x => x.StartTime)
                .Empty()
                    .When(x => validAppointmentTypeForNullTime.Contains(x.AppointmentType))
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("StartTime"));

            When(x => TimeOnly.TryParse(x.StartTime, out TimeOnly result), () =>
            {
                RuleFor(x => TimeOnly.Parse(x.StartTime))
                    .LessThanOrEqualTo(x => TimeOnly.Parse(x.EndTime))
                    .When(x => x.AppointmentType == AppointmentType.BetweenPatients)
                        .WithMessage(ValidationMessage.LessOrEqual.For("StartTime", "EndTime"))
                    .LessThan(x => TimeOnly.Parse(x.EndTime))
                    .When(x => x.AppointmentType != AppointmentType.BetweenPatients)
                        .WithMessage(ValidationMessage.LessThan.For("StartTime", "EndTime"));
            }).Otherwise(() =>
            {
                RuleFor(x => x.StartTime)
                    .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("StartTime"));
            });

            RuleFor(x => x.EndTime)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("EndTime"))
                .Empty()
                    .When(x => validAppointmentTypeForNullTime.Contains(x.AppointmentType));

            When(x => TimeOnly.TryParse(x.EndTime, out TimeOnly result), () =>
            {
                RuleFor(x => TimeOnly.Parse(x.EndTime))
                    .GreaterThanOrEqualTo(x => TimeOnly.Parse(x.StartTime))
                    .When(x => x.AppointmentType == AppointmentType.BetweenPatients)
                        .WithMessage(ValidationMessage.GreaterOrEqual.For("EndTime", "StartTime"))
                    .GreaterThan(x => TimeOnly.Parse(x.StartTime))
                    .When(x => x.AppointmentType != AppointmentType.BetweenPatients)
                        .WithMessage(ValidationMessage.GreaterThan.For("EndTime", "StartTime"));
            }).Otherwise(() =>
            {
                RuleFor(x => x.EndTime)
                    .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("EndTime"));
            });

            RuleFor(x => x.RoomId)
                .Empty()
                .When(m => m.DeviceId == null)
                .WithMessage("{PropertyName} is required if serviceId is null");
        }
    }
}

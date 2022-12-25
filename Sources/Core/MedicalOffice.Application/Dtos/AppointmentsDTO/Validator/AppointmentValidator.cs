using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AppointmentValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentValidator()
        {
            var validAppointmentTypeForNullTime = new AppointmentType[] { AppointmentType.waiting };
            var invalidAppointmentTypes = new AppointmentType[] { AppointmentType.FinalApproval,AppointmentType.Canceled};

            Include(new IPhoneNumberValidator());
            Include(new INationalIdValidator());
            Include(new IServiceIdValidator());
            Include(new IMedicalStaffValidator());

            RuleFor(x => x.AppointmentType)
                .NotEmpty()
                    .WithMessage("")
                .Must(x => !invalidAppointmentTypes.Contains(x))
                    .WithMessage("{FinalApproval and Canceled should not be used in adding appointment process}");

            RuleFor(x => x.PatientName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("PatientName"));

            RuleFor(x => x.PatientLastName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("PatientLastName"));

            RuleFor(x=>x.StartTime)
                .Empty()
                    .When(x=>validAppointmentTypeForNullTime.Contains(x.AppointmentType))
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("StartTime"))
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("StartTime"));
            RuleFor(x => TimeOnly.Parse(x.StartTime))
                .LessThanOrEqualTo(x => TimeOnly.Parse(x.EndTime))
                .When(x=>x.AppointmentType==AppointmentType.BetweenPatients)
                    .WithMessage(ValidationMessage.LessOrEqual.For("StartTime", "EndTime"))
                .LessThan(x=> TimeOnly.Parse(x.EndTime))
                .When(x=>x.AppointmentType!=AppointmentType.BetweenPatients)
                    .WithMessage(ValidationMessage.LessThan.For("StartTime", "EndTime"));

            RuleFor(x => x.EndTime)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("EndTime"))
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("EndTime"))
                .Empty()
                    .When(x => validAppointmentTypeForNullTime.Contains(x.AppointmentType));
            RuleFor(x => TimeOnly.Parse(x.EndTime))
                .GreaterThanOrEqualTo(x => TimeOnly.Parse(x.StartTime))
                .When(x => x.AppointmentType == AppointmentType.BetweenPatients)
                    .WithMessage(ValidationMessage.GreaterOrEqual.For("EndTime", "StartTime"))
                .GreaterThan(x => TimeOnly.Parse(x.StartTime))
                .When(x => x.AppointmentType != AppointmentType.BetweenPatients)
                    .WithMessage(ValidationMessage.GreaterThan.For("EndTime", "StartTime"));

            RuleFor(x => x.RoomId)
                .Empty()
                .When(m => m.DeviceId == null)
                .WithMessage("{PropertyName} is required if serviceId is null");
        }
    }
}

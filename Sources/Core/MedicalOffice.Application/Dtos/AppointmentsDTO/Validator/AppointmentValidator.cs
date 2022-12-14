using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
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
            Include(new IPhoneNumberValidator());
            Include(new INationalIdValidator());

            RuleFor(x => x.PatientName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For<AppointmentDTO>(p => p.PatientName));

            RuleFor(x => x.PatientLastName)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For<AppointmentDTO>(p => p.PatientLastName));

            RuleFor(x => x.StartTime)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For<AppointmentDTO>(p => p.StartTime))
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For<AppointmentDTO>(p => p.StartTime));

            RuleFor(x => TimeOnly.Parse(x.StartTime))
                .LessThan(x => TimeOnly.Parse(x.EndTime))
                    .WithMessage(ValidationMessage.LessThan.For<AppointmentDTO>(p => p.StartTime, x => x.EndTime));

            RuleFor(x => x.EndTime)
             .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For<AppointmentDTO>(p => p.EndTime))
             .NotEmpty()
                .WithMessage(ValidationMessage.Required.For<AppointmentDTO>(p => p.EndTime));

            RuleFor(x => TimeOnly.Parse(x.EndTime))
               .GreaterThan(x => TimeOnly.Parse(x.StartTime))
                  .WithMessage(ValidationMessage.GreaterThan.For<AppointmentDTO>(p => p.EndTime, x => x.StartTime));

            RuleFor(x => x.DeviceId)
               .Empty()
               .When(m => m.RoomId == null)
               .WithMessage("{PropertyName} is required if serviceId is null");

            RuleFor(x => x.RoomId)
                .Empty()
                .When(m => m.DeviceId == null)
                .WithMessage("{PropertyName} is required if serviceId is null");
        }
    }
}

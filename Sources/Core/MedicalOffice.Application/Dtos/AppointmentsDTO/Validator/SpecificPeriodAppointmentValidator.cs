using FluentValidation;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class SpecificPeriodAppointmentValidator : AbstractValidator<SpecificPeriodAppointmentDTO>
    {
        public SpecificPeriodAppointmentValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.MedicalStaffId).NotEmpty()
                .When(x => x.DeviceId != null);

            RuleFor(x=>x.DeviceId).NotEmpty()
                .When(x=>x.RoomId!=null);
            RuleFor(x=>x.DeviceId).Empty()
                .When(x=>x.RoomId != null); 
        }
    }
}

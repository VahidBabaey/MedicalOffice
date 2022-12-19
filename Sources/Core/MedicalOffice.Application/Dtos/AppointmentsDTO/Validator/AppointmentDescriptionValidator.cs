using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AppointmentDescriptionValidator: AbstractValidator<AppointmentDescriptionDTO>
    {
        public AppointmentDescriptionValidator()
        {
            RuleFor(x=>x.AppointmentId).NotEmpty();
            RuleFor(x=>x.Description).NotEmpty();   
        }
    }
}

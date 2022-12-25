using FluentValidation;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AppointmentTypeValidator : AbstractValidator<AppointmentTypeDTO>
    {
        public AppointmentTypeValidator()
        {
            var validTypesToUpadete= new AppointmentType[] { AppointmentType.FinalApproval, AppointmentType.Canceled};

            Include(new IAppointmentIdValidator());
            RuleFor(x => x.AppointmentType)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required")
                .Must(m => validTypesToUpadete.Contains(m))
                    .WithMessage("{PropertyName} to change should be choosen between finalApproval or Canceled type");
        }
    }
}

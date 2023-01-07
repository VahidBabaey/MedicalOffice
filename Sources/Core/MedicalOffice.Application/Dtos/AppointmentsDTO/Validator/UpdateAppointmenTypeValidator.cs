using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AppointmentTypeValidator : AbstractValidator<UpdateAppointmentTypeDTO>
    {
        private IAppointmentRepository _appointmentRepository;
        private IOfficeResolver _officeResolver;

        public AppointmentTypeValidator(IAppointmentRepository appointmentRepository, IOfficeResolver officeResolver)
        {
            _appointmentRepository = appointmentRepository;
            _officeResolver = officeResolver;

            var validTypesToUpadete= new AppointmentType[] { AppointmentType.FinalApproval, AppointmentType.Canceled};

            Include(new AppointmentIdValidator(_appointmentRepository, _officeResolver));
            RuleFor(x => x.AppointmentType)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required")
                .Must(m => validTypesToUpadete.Contains(m))
                    .WithMessage("{PropertyName} to change should be choosen between finalApproval or Canceled type");
        }
    }
}

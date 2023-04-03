using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class UpdateAppointmentDescriptionValidator: AbstractValidator<UpdateAppointmentDescriptionDTO>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IContextResolver _officeResolver;
        public UpdateAppointmentDescriptionValidator(IAppointmentRepository appointmentRepository, IContextResolver officeResolver)
        {
            _appointmentRepository = appointmentRepository;
            _officeResolver = officeResolver;

            Include(new AppointmentIdValidator(_appointmentRepository,_officeResolver));
            RuleFor(x=>x.Description).NotEmpty();   
        }
    }
}

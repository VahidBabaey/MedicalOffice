using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons.validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class TransferAppointmentValidator : AbstractValidator<TransferAppointmentDTO>
    {
        private IAppointmentRepository _appointmentRepository;
        private IOfficeResolver _officeResolver;
        public TransferAppointmentValidator(
             IAppointmentRepository appointmentRepository,IOfficeResolver officeResolver)
        {
            _appointmentRepository = appointmentRepository;
            _officeResolver = officeResolver;

            Include(new AppointmentIdValidator(_appointmentRepository, _officeResolver));
        }
    }
}

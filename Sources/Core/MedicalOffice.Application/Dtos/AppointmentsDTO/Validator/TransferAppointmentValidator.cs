using FluentValidation;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class TransferAppointmentValidator : AbstractValidator<TransferAppointmentDTO>   
    {
        public TransferAppointmentValidator()
        {
            Include(new IAppointmentIdValidator());
        }
    }
}

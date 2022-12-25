using FluentValidation;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class PatientInfoValidator:AbstractValidator<PatientInfoDTO>
    {
        public PatientInfoValidator()
        {
            Include(new IAppointmentIdValidator());
            Include(new IPhoneNumberValidator());
            Include(new INationalIdValidator());
        }
    }
}

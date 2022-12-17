using FluentValidation;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class SpecificDateAppointmentValidator : AbstractValidator<SpecificDateAppointmentDTO>
    {
        public SpecificDateAppointmentValidator()
        {

        }
    }
}

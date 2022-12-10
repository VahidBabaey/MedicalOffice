using FluentValidation;
using MedicalOffice.Application.Constants;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class SearchAppointmentValidator : AbstractValidator<SearchAppointmentsDTO>
    {
        public SearchAppointmentValidator()
        {
            RuleForEach(x => x.FilterFields)
                .SetValidator(new FilterFieldsValidator());
        }
    }

    public class FilterFieldsValidator : AbstractValidator<FilterFields>
    {
        public FilterFieldsValidator()
        {
            RuleFor(x => x.MedicalStaffId)
                .NotEmpty()
                .When(m => m.ServiceId != null)
                .WithMessage("{PropertyName} is required if serviceId is null");

            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .When(m => m.MedicalStaffId != null)
                .WithMessage("{PropertyName} is required if medcalStaffId is null");
        }
    }
}

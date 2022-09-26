﻿using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command
{
    public class DeletePatientIllnessFormCommand : IRequest<BaseCommandResponse>
    {
        public Guid PatientIllnessFormId { get; set; }
    }
}

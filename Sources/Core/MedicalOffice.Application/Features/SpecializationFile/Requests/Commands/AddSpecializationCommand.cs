﻿using MediatR;
using MedicalOffice.Application.Dtos.Specialization;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.SpecializationFile.Requests.Commands
{
    public class AddSpecializationCommand : IRequest<BaseCommandResponse>
    {
        public SpecializationDTO DTO { get; set; } = new SpecializationDTO();
    }
}

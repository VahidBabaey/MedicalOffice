﻿using MediatR;
using MedicalOffice.Application.Dtos.Insurance;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;


public class EditInsuranceCommand : IRequest<BaseCommandResponse>
{
    public UpdateInsuranceDTO DTO { get; set; } = new UpdateInsuranceDTO();
}

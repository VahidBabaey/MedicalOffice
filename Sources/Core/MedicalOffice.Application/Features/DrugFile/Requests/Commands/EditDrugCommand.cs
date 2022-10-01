﻿using MediatR;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Commands
{
    public class EditDrugCommand : IRequest<BaseCommandResponse>
    {
        public UpdateDrugDTO DTO { get; set; } = new UpdateDrugDTO();
    }
}

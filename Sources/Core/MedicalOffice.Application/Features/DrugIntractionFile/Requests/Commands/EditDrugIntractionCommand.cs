﻿using MediatR;
using MedicalOffice.Application.Dtos.DrugIntractionD;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugIntractionFile.Requests.Commands
{
    public class EditDrugIntractionCommand : IRequest<BaseCommandResponse>
    {
        public UpdateDrugIntractionDTO DTO { get; set; } = new UpdateDrugIntractionDTO();
    }
}

﻿using MediatR;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Commands
{
    public class AddDrugCommand : IRequest<BaseResponse>
    {
        public DrugDTO DTO { get; set; } = new DrugDTO();
        public Guid OfficeId { get; set; }
    }
}

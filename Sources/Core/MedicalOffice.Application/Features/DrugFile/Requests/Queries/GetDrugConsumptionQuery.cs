﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Queries
{
    public class GetDrugConsumptionQuery : IRequest<List<DrugConsumptionListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}

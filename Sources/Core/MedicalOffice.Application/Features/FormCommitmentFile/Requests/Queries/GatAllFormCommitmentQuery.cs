﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries
{
    public class GatAllFormCommitmentQuery : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();

        public Guid OfficeId { get; set; }
        public Order? Order { get; set; }
    }
}

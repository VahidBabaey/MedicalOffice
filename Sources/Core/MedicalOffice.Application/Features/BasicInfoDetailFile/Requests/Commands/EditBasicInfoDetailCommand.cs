﻿using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands
{
    public class EditBasicInfoDetailCommand : IRequest<BaseResponse>
    {
        public UpdateBasicInfoDetailDTO DTO { get; set; } = new UpdateBasicInfoDetailDTO();
        public Guid OfficeId { get; set; }
    }
}

﻿using MediatR;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Commands
{
    public class AddOfficeCommand : IRequest<BaseResponse>
    {
        public AddOfficeDto DTO { get; set; }
    }
}
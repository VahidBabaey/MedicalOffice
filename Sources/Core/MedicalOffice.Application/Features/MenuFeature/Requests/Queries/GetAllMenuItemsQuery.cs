﻿using MediatR;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.MenuFeature.Requests.Queries
{
    public class GetAllMenuItemsQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
    }
}
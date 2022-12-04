﻿using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class GetUserStatusQuery : IRequest<BaseResponse>
    {
        public PhoneNumberDTO DTO { get; set; } = new();
    }
}
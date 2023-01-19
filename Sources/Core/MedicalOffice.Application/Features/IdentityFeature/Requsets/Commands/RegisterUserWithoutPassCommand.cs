﻿using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class RegisterUserWithoutPassCommand : IRequest<BaseResponse>
    {
        public RegisterUserWithoutPassDTO DTO { get; set; }
    }
}
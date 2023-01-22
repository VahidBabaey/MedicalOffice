﻿using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Queries
{
    public class GetAllTariffByServiceIDQuery : IRequest<BaseResponse>
    {
        public TariffListDTO DTO { get; set; } = new TariffListDTO();
        public Guid OfficeId { get; set; }
        public Guid ServiceId { get; set; } 
    }
}

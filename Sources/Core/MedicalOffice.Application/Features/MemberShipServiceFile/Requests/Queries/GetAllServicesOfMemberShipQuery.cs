﻿using MediatR;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries
{
    public class GetAllServicesOfMemberShipQuery : IRequest<List<ServicesOfMemeberShipListDTO>>
    {
        public Guid MemberShipId { get; set; }
        public Guid OfficeId { get; set; }
    }
}

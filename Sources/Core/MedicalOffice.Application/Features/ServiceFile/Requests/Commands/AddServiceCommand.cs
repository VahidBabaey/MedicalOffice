﻿using MediatR;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Commands
{

    public class AddServiceCommand : IRequest<BaseCommandResponse>
    {
        public ServiceDTO Dto { get; set; } = new ServiceDTO();
    }
}

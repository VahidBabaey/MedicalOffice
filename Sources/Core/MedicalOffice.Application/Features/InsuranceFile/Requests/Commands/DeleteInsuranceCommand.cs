﻿using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;


public class DeleteInsuranceCommand : IRequest<BaseResponse>
{
    public Guid InsuranceID { get; set; }
    public Guid OfficeId { get; set; }
}

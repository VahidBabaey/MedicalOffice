﻿using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Request.Commands
{
    public class AddCashMoneyCommand : IRequest<BaseResponse>
    {
        public CashMoneyDTO DTO { get; set; } = new CashMoneyDTO();
        public Guid OfficeId { get; set; }
    }
}

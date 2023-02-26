﻿using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands
{
    public class EditMedicalStaffScheduleCommand : IRequest<BaseResponse>
    {
        public MedicalStaffScheduleDTO DTO { get; set; } = new MedicalStaffScheduleDTO();
        public Guid OfficeId { get; set; }
    }
}

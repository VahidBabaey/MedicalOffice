using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands
{
    public class AddMedicalStaffWorkHoursProgramCommand : IRequest<BaseCommandResponse>
    {
        public MedicalStaffWorkHoursProgramDTO DTO { get; set; } = new MedicalStaffWorkHoursProgramDTO();
    }
}

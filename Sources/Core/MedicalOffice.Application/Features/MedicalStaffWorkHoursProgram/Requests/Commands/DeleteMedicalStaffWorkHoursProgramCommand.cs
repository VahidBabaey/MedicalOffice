using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands
{
    public class DeleteMedicalStaffWorkHoursProgramCommand : IRequest<BaseCommandResponse>
    {
        public Guid MedicalStaffId { get; set; }
    }
}

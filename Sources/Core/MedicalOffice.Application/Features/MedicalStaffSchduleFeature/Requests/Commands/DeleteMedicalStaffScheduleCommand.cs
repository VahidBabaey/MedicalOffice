using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands
{
    public class DeleteMedicalStaffScheduleCommand : IRequest<BaseResponse>
    {
        public Guid MedicalStaffId { get; set; }
    }
}

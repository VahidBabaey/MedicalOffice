using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands
{
    public class DeleteMedicalStaffCommand : IRequest<BaseResponse>
    {
        public Guid UserId { get; set; }
    }
}

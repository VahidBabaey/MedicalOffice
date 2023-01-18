using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries
{
    public class GetAllBasicInfoQuery : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
    }
}

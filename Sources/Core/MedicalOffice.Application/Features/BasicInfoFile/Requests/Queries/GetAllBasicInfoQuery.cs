using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries
{
    public class GetAllBasicInfoQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public Order? Order { get; set; }
    }
}

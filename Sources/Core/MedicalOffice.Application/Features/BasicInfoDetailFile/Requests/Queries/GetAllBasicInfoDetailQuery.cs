using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries
{
    public class GetAllBasicInfoDetailQuery : IRequest<List<BasicInfoDetailListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid BasicInfoId { get; set; }
    }
}

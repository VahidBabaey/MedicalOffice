using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Queries
{
    public class GetAllTariffByServiceIDQuery : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
        public ServiceIdDTO ServiceId { get; set; } 
    }
}

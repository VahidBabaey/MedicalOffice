using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Responses;
using System.Collections.Generic;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Queries
{
    public class GetDrugBySearchQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
    }
}

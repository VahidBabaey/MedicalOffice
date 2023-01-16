using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using System.Collections.Generic;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Queries
{
    public class GetDrugBySearchQuery : IRequest<List<DrugListDTO>>
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
    }
}

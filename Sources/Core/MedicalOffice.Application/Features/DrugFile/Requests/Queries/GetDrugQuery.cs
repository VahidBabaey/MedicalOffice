using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using System.Collections.Generic;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Queries
{
    public class GetDrugQuery : IRequest<List<DrugListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}

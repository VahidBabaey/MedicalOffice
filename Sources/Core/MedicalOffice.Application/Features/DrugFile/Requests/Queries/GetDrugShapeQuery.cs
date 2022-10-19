using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Requests.Queries
{
    public class GetDrugShapeQuery : IRequest<List<DrugShapeListDTO>>
    {
      public ListDto DTO { get; set; } = new ListDto();
    }

}

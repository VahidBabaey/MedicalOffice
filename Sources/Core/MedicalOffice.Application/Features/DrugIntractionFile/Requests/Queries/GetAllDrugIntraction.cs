using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugIntractionFile.Requests.Queries
{
    public class GetAllDrugIntraction : IRequest<List<DrugIntractionListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}

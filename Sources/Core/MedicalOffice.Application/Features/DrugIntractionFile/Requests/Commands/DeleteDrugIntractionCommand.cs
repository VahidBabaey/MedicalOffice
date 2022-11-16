using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugIntractionFile.Requests.Commands
{
    public class DeleteDrugIntractionCommand : IRequest<BaseResponse>
    {
        public Guid DrugIntractionID { get; set; }
    }
}

using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Handlers.Queries
{
    public class DeleteDrugCommand : IRequest<BaseResponse>
    {
        public Guid DrugId { get; set; }
    }
}

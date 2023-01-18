using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.DrugIntractionFile.Requests.Queries
{
    public class GetAllDrugIntraction : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}

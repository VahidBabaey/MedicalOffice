using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class GetDoctorDateTimesQuery : IRequest<BaseResponse>
    {
        public DoctorTimesDTO DTO { get; set; }
    }
}
using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands
{
    public class EditAppointmentPatientCommand : IRequest<BaseResponse>
    {
        public PatientInfoDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}
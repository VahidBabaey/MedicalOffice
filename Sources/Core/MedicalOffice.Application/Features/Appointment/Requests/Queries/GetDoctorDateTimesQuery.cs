using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.Appointment.Requests.Queries
{
    public class GetDoctorDateTimesQuery : IRequest<BaseResponse>
    {
        public DateAppointmentDto DTO { get; set; }
    }
}
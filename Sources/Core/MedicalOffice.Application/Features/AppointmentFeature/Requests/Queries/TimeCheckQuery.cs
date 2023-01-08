using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class TimeCheckQuery : IRequest<BaseResponse>
    {
        public CheckTimeRequestDTO DTO { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid? MedicalStaffId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime Date { get; set; }
        public Guid OfficeId { get; set; }
    }
}
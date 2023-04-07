using MedicalOffice.Application.Dtos;

using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IAppointmentRepository : IGenericRepository<Appointment, Guid>
    {
        Task<List<AppointmentDetailsDTO>> GetByDateAndStaff(DateTime dateTime, Guid? serviceId = null, Guid? medicalStaffId = null);
        Task<List<AppointmentDetailsDTO>> GetByDateAndDevice(DateTime date, Guid? deviceId = null, Guid? roomId = null);
        Task<List<AppointmentDetailsDTO>> GetByStaffAndDevice(DateTime date, Guid? deviceId = null, Guid? medicalStaffId = null);
        Task<List<AppointmentDetailsDTO>> GetByServiceAndDevice(DateTime date, Guid? serviceId, Guid? deviceId);
        Task<List<AppointmentDetailsDTO>> GetByPeriodAndStaff(DateTime startDate, DateTime endDate, Guid? medicalStaffId);
        Task<List<AppointmentDetailsDTO>> GetByPeriodAndDeviceId(DateTime startDate, DateTime endDate, Guid? deviceId);
        Task<List<AppointmentDetailsDTO>> searchPatientAappointments(string input, DateTime? date, Guid officeId);
        Task<bool> checkAppointmentExist(Guid appointmentId, Guid officeId);
    }
}

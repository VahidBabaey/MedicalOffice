using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffScheduleRepository : IGenericRepository<MedicalStaffSchedule, Guid>
    {
        Task DeleteMedicalStaffSchedule(Guid id);

        Task<List<MedicalStaffSchedule>> GetMedicalStaffScheduleByStaffId(Guid? medicalStaffId, Guid OfficeId);

        Task UpdateMedicalStaffsSchedule(Guid MedicalStaffid, int day, MedicalStaffScheduleDTO MedicalStaffSchedule);

        Task<List<Guid>> AddRangle(List<MedicalStaffSchedule> MedicalStaffSchedules);

        Task UpdateRange(List<MedicalStaffSchedule> MedicalStaffSchedules);

        Task<MedicalStaffSchedule> GetStaffScheduleByDate(Guid? medicalStaffId, DayOfWeek dayOfweek);

        Task<bool> CheckTimeIsInStaffSchedule(Guid medicalStaffId, DateTime date);
    }
}

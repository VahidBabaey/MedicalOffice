using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffScheduleRepository : IGenericRepository<MedicalStaffSchedule, Guid>
    {
        Task DeleteMedicalStaffSchedule(Guid id);
        
        Task<IReadOnlyList<MedicalStaffSchedule>> GetMedicalStaffScheduleByID(Guid? Id);
        
        Task UpdateMedicalStaffsSchedule(Guid MedicalStaffid, int day, MedicalStaffScheduleDTO MedicalStaffSchedule);

        Task<List<Guid>> AddRangle(List<MedicalStaffSchedule> MedicalStaffSchedules);

        Task DeleteRangle(List<MedicalStaffSchedule> MedicalStaffSchedules);   

        Task<MedicalStaffScheduleDayOfWeekDTO> GetStaffScheduleByDate(Guid? medicalStaffId,DayOfWeek dayOfweek);    
    }
}

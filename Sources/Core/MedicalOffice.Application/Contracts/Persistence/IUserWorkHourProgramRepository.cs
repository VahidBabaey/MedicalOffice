using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffWorkHourProgramRepository : IGenericRepository<MedicalStaffWorkHourProgram, Guid>
    {
        Task DeleteMedicalStaffWorkHourProgram(Guid id);
        Task<IReadOnlyList<MedicalStaffWorkHourProgram>> GetMedicalStaffWorkHourProgramByID(Guid Id);
        Task UpdateMedicalStaffsWorkHoursProgram(Guid MedicalStaffid, int day, MedicalStaffWorkHoursProgramDTO MedicalStaffWorkHoursProgramDTO);
    }
}

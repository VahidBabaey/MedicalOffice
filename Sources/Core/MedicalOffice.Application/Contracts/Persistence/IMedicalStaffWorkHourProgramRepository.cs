using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffWorkHourProgramRepository : IGenericRepository<MedicalStaffWorkHourProgram, Guid>
    {
        Task DeleteMedicalStaffWorkHourProgram(Guid id);
        Task<IReadOnlyList<MedicalStaffWorkHourProgram>> GetMedicalStaffWorkHourProgramByID(Guid Id);

        //Task<string> GetAllMedicalStaffWorkHoursProgram(Guid id);
        Task UpdateMedicalStaffsWorkHoursProgram(Guid userid, int day, MedicalStaffWorkHoursProgramDTO medicalStaffWorkHoursProgramDTO);
    }
}

using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserWorkHourProgramRepository : IGenericRepository<UserWorkHourProgram, Guid>
    {
        Task DeleteUserWorkHourProgram(Guid id);
        Task<IReadOnlyList<UserWorkHourProgram>> GetUserWorkHourProgramByID(Guid Id);
        Task UpdateUsersWorkHoursProgram(Guid userid, int day, UserWorkHoursProgramDTO UserWorkHoursProgramDTO);
    }
}

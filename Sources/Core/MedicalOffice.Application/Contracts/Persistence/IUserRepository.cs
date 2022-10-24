using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId);
        Task<IEnumerable<MedicalStaffListDTO>> GetAllUsers();
        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllUsersName();
        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId);
        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId);
    }
}

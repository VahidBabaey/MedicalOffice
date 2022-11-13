using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId);

        Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs();

        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllUsersName();


        Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber);

        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId);

        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId);

        //Task<MedicalStaff> UpdateMedicalStaffPermissions(Guid medicalStaffId, List<MedicalStaffPermission> medicalStaffPermissions);

        Task MedicalStaffPermissions(Guid userId, Guid officeId);
    }
}

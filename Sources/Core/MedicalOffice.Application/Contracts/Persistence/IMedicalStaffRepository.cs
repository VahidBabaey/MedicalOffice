using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRepository : IGenericRepository<MedicalStaff, Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId);

        Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber);

        Task UpdateUserOfficeRoleAsync(Guid roleId, Guid UserId);

        Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName();

        Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles();

        Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId);
        Task<IEnumerable<MedicalStaffListDTO>> GetAllMedicalStaffs();
    }
}

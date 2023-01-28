using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User> InsertToUser(Guid officeid);

        Task<User?> CheckByPhoneOrNationalId(string phoneNumber, string nationalId);
    }
}

using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User> InsertToUser(Guid officeid);

        Task<User?> CheckByPhoneOrNationalId(string phoneNumber, string nationalId);

        Task<User?> FindExistAndActiveUser(string phoneNumber, bool isActive);

        Task<User?> CheckByPhoneAndNationalId(string phoneNumber, string nationalID);
        Task<User?> GetUserByPhoneNumber(string phoneNumber);
        Task<User?> GetUserByNationalId(string nationalId);
    }
}
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientRepository : IGenericRepository<Patient, Guid>
    {
        Task<Patient?> GetByNationalCode(string nationalCode);
        Task<Patient?> GetByPhoneNumber(string phoneNumber);
        Task<Patient?> GetByFileNumber(string fileNumber);
        Task<IReadOnlyList<Patient>> GetPatientsByFileNumber(string fileNumber);
        Task<IReadOnlyList<Patient>> GetPatientsByNationalCode(string nationalid);
        Task<IReadOnlyList<Patient>> GetPatientsByPhoneNumber(string phonenumber);

        

    }
}

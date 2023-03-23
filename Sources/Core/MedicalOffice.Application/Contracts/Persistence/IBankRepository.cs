using MedicalOffice.Application.Dtos.BankDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IBankRepository : IGenericRepository<Bank, Guid>
    {
        Task<List<BankListDTO>> GetAllBankNames();
    }
}

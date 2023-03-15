using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BankDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class BankRepository : GenericRepository<Bank, Guid>, IBankRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BankRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<BankListDTO>> GetAllBankNames()
    {
        var bankNamesDTO = new List<BankListDTO>();

        var listbank = await _dbContext.Banks.ToListAsync();

        foreach (var item in listbank)
        {
            var bankNames = new BankListDTO()
            {
                Id = item.Id,
                BankName = item.BankName,
            };
            bankNamesDTO.Add(bankNames);
        }

        return bankNamesDTO;
    }
}

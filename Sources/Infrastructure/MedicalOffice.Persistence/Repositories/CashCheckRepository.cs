using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashCheckRepository : GenericRepository<CashCheck, Guid>, ICashCheckRepository
{
    private readonly IGenericRepository<CashCheck, Guid> _cashCheckRepository;
    private readonly ApplicationDbContext _dbContext;

    public CashCheckRepository(IGenericRepository<CashCheck, Guid> cashCheckRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashCheckRepository = cashCheckRepository;
    }
    public async Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.OfficeId == officeId && p.Id == receptonId);
        return isExist;
    }
    public async Task<bool> CheckExistCashId(Guid officeId, Guid cashId)
    {
        bool isExist = await _dbContext.Cashes.AnyAsync(p => p.OfficeId == officeId && p.Id == cashId);
        return isExist;
    }
    public async Task<bool> CheckCashCheckId(Guid cashCheckId)
    {
        bool isExist = await _dbContext.CashChecks.AnyAsync(p => p.Id == cashCheckId);
        return isExist;
    }

}

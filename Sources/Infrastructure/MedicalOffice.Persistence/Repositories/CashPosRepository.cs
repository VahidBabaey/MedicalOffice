using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashPosRepository : GenericRepository<CashPos, Guid>, ICashPosRepository
{
    private readonly IGenericRepository<CashPos, Guid> _cashPosRepository;
    private readonly ApplicationDbContext _dbContext;

    public CashPosRepository(IGenericRepository<CashPos, Guid> cashPosRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashPosRepository = cashPosRepository;
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
    public async Task<bool> CheckCashPosId(Guid cashPosId)
    {
        bool isExist = await _dbContext.CashPoses.AnyAsync(p => p.Id == cashPosId);
        return isExist;
    }

}

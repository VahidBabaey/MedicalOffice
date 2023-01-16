using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class CashCartRepository : GenericRepository<CashCart, Guid>, ICashCartRepository
{
    private readonly IGenericRepository<CashCart, Guid> _cashCartRepository;
    private readonly ApplicationDbContext _dbContext;

    public CashCartRepository(IGenericRepository<CashCart, Guid> cashCartRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _cashCartRepository = cashCartRepository;
    }

    public async Task<bool> CheckExistReceptionId(Guid officeId,Guid receptonId)
    {
        bool isExist = await _dbContext.Receptions.AnyAsync(p => p.OfficeId == officeId && p.Id == receptonId);
        return isExist;
    }
    public async Task<bool> CheckExistCashId(Guid officeId, Guid cashId)
    {
        bool isExist = await _dbContext.Cashes.AnyAsync(p => p.OfficeId == officeId && p.Id == cashId);
        return isExist;
    }
    public async Task<bool> CheckCashCartId(Guid cashCartId)
    {
        bool isExist = await _dbContext.CashCarts.AnyAsync(p => p.Id == cashCartId);
        return isExist;
    }
}

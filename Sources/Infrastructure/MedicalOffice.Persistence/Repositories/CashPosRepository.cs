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

    public async void UpdateCashReception(Guid cashId, decimal cashvalue)
    {
        var cash = _dbContext.Cashes.Where(p => p.Id == cashId).FirstOrDefault();
        //cash.Recieved = 

    }
}

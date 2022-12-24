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

}

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

}

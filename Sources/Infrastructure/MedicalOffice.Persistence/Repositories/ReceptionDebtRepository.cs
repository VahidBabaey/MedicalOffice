using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionDebtRepository : GenericRepository<ReceptionDebt, Guid>, IReceptionDebtRepository
{
    private readonly IGenericRepository<ReceptionDebt, Guid> _receptionReceptionDebt;
    private readonly ApplicationDbContext _dbContext;

    public ReceptionDebtRepository(IGenericRepository<ReceptionDebt, Guid> receptionReceptionDebt, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _receptionReceptionDebt = receptionReceptionDebt;
    }

    public async Task<Guid> AddReceptionDebt(Guid receptionId, Guid receptionDetailId, Guid officeId, float receptionDebtPrice)
    {
        ReceptionDebt receptionDebt = new()
        {
            ReceptionId = receptionId,
            ReceptionDetailId = receptionDetailId,
            OfficeId = officeId,
            ReceptionDebtPrice = receptionDebtPrice,
        };
        await _receptionReceptionDebt.Add(receptionDebt);

        return receptionDebt.Id;
    }
}

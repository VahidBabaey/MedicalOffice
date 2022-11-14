using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ReceptionDiscountRepository : GenericRepository<ReceptionDiscount, Guid>, IReceptionDiscountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReceptionDiscountRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

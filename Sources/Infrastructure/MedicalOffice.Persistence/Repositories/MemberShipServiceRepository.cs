using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MemberShipServiceRepository : GenericRepository<MemberShipService, Guid>, IMembershipServiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MemberShipServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

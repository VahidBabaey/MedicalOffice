using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class BasicInfoRepository : GenericRepository<BasicInfo, Guid>, IBasicInfoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BasicInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class BasicInfoDetailRepository : GenericRepository<BasicInfoDetail, Guid>, IBasicInfoDetailRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BasicInfoDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId(Guid basicInfoId)
    {
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == basicInfoId).ToListAsync();
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoIllnessId()
    {
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("451a9dda-5efe-45cb-8754-3c51e01f9cf0")).ToListAsync();
    }
}

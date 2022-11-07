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
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("35cc078c-928d-4e64-9229-b6a1c6969f23")).ToListAsync();
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoCommitmentId()
    {
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == Guid.Parse("7d4395ec-e818-46bd-9500-b47446fdc8c8")).ToListAsync();
    }
}

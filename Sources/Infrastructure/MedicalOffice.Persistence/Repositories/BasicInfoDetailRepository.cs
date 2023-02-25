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
        return await _dbContext.BasicInfoDetail.Where(srv => srv.basicInfoId == basicInfoId && srv.IsDeleted == false).ToListAsync();
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoIllnessId()
    {
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == Guid.Parse("e8eec908-b1b8-4999-bec2-8b93cda1626d") && p.IsDeleted == false).ToListAsync();
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoCommitmentId()
    {
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == Guid.Parse("7d4395ec-e818-46bd-9500-b47446fdc8c8") && p.IsDeleted == false).ToListAsync();
    }
    public async Task<bool> CheckExistBasicInfoId(Guid officeId, Guid basicInfoId)
    {
        bool isExist = await _dbContext.BasicInfos.AnyAsync(p => p.OfficeId == officeId && p.Id == basicInfoId);
        return isExist;
    }
    public async Task<bool> CheckExistBasicInfoDetailId(Guid basicInfoDetailId)
    {
        bool isExist = await _dbContext.BasicInfoDetail.AnyAsync(p => p.Id == basicInfoDetailId);
        return isExist;
    }
    public async Task<bool> CheckExistBasicInfoDetailName(string basicinfodetailName, Guid BasicInfoId)
    {
        bool isExist = await _dbContext.BasicInfoDetail.AnyAsync(p => p.InfoDetailName == basicinfodetailName && p.basicInfoId == BasicInfoId && p.IsDeleted == false);
        return isExist;
    }
}

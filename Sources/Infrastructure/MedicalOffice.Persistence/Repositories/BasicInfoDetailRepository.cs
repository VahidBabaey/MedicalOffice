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
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == basicInfoId && p.IsDeleted == false).OrderByDescending(p => p.CreatedDate).ToListAsync();
    }
    public async Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoIllnessId()
    {
        return await _dbContext.BasicInfoDetail.Where(p => p.basicInfoId == Guid.Parse("e8eec908-b1b8-4999-bec2-8b93cda1626d") && p.IsDeleted == false).ToListAsync();
    }
    public async Task<IReadOnlyList<FormCommitment>> GetFormCommitments(Guid officeId)
    {
        return await _dbContext.FormCommitments.Where(p => p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
    }
    public async Task<bool> CheckExistBasicInfoId(Guid officeId, Guid basicInfoId)
    {
        bool isExist = await _dbContext.BasicInfos.AnyAsync(p => p.Id == basicInfoId);
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

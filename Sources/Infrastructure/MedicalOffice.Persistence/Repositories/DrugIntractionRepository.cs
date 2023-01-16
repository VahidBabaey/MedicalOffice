using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugIntractionRepository : GenericRepository<DrugIntraction, Guid>, IDrugIntractionRepository
{
    private readonly ApplicationDbContext _dbContext;
    public DrugIntractionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<DrugIntractionListDTO>> GetAllDrugIntractions()
    {
        var _list = await _dbContext.DrugIntractions.Select(p => new DrugIntractionListDTO
        {
            Id = p.Id,
            Group1 = p.Group1,
            Group2 = p.Group2,
            Effects = p.Effects,
            Method = p.Method,
            Control = p.Control,
            PDrugId = p.PDrugId,
            PDrugName = _dbContext.Drugs.Select(q => new { q.Id, q.Name }).Where(q => q.Id == p.PDrugId).FirstOrDefault().Name,
            SDrugId = p.SDrugId,
            SDrugName = _dbContext.Drugs.Select(q => new { q.Id, q.Name }).Where(q => q.Id == p.SDrugId).FirstOrDefault().Name,

        }).ToListAsync();

        return (IEnumerable<DrugIntractionListDTO>)_list;
    }
    public async Task<bool> CheckExistDrugIntractionId(Guid drugIntractionId)
    {
        bool isExist = await _dbContext.DrugIntractions.AnyAsync(p => p.Id == drugIntractionId);
        return isExist;
    }
    public async Task<bool> CheckExistDrugId(Guid drugIntractionId)
    {
        bool isExist = await _dbContext.Drugs.AnyAsync(p => p.Id == drugIntractionId);
        return isExist;
    }
}

using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MedicalOffice.Persistence.Repositories;

public class SectionRepository : GenericRepository<Section, Guid>, ISectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistSectionId(Guid officeId, Guid sectionId)
    {
        bool isExist = await _dbContext.Sections.AnyAsync(p => p.Id == sectionId && p.OfficeId == officeId);
        return isExist;
    }
    public async Task<List<Section>> GetSectionBySearch(string name, int take, int skip)
    {
        var sections = await _dbContext.Sections.Where(p => p.Name.Contains(name)).Take(take).Skip(skip).ToListAsync();
        return sections;
    }
    public async Task<List<SectionNamesListDTO>> GetSectionNames(Guid officeId)
    {
        List<SectionNamesListDTO> sectionNamesListDTOs = new List<SectionNamesListDTO>();
        var sections = await _dbContext.Sections.Where(p => p.Status == true && p.OfficeId == officeId && p.IsDeleted == false).ToListAsync();
        foreach (var item in sections)
        {
            SectionNamesListDTO sectionNamesListDTO = new()
            {
                Id = item.Id,
                Name = item.Name,
            };
            sectionNamesListDTOs.Add(sectionNamesListDTO);
        }
        return sectionNamesListDTOs;  
    }
}

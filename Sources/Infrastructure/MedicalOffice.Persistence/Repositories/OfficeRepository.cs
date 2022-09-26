using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Persistence.Repositories;

public class OfficeRepository : GenericRepository<Office, Guid>, IOfficeRepository
{
    public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
    { 
    }
}

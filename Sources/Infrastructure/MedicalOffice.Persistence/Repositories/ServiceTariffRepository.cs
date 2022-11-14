using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ServiceTariffRepository : GenericRepository<Tariff, Guid>, IServiceTariffRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceTariffRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

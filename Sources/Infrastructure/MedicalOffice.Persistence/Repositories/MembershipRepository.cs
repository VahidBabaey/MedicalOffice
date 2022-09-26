using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MembershipRepository : GenericRepository<Membership, Guid>, IMembershipRepository
{
    private readonly IServiceRepository _repository;
    private readonly ApplicationDbContext _dbContext;

    public MembershipRepository(IServiceRepository repositoryservice, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repository = repositoryservice;
        _dbContext = dbContext;
    }

    public async Task<Service> InsertMembershipIdofServiceAsync(Guid serviceId, Guid membershipId)
    {
        var service = await _dbContext.Services.SingleOrDefaultAsync(srv => srv.Id == serviceId);

        if (service == null)
            throw new Exception("");

        service.MembershipId = membershipId;
        _repository.Update(service);
        return service;
    }
    public async Task<Service> DeleteMembershipIdofServiceAsync(Guid serviceId, Guid membershipId)
    {
        var service = await _dbContext.Services.SingleOrDefaultAsync(srv => srv.Id == serviceId);

        if (service == null)
            throw new Exception("");

        service.MembershipId = null;
        _repository.Delete(service);
        return service;
    }
    public async Task<Service> UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId)
    {
        var service = await _dbContext.Services.SingleOrDefaultAsync(srv => srv.Id == serviceId);

        if (service == null)
            throw new Exception("");

        service.MembershipId = membershipId;
        _repository.Update(service);
        return service;
    }
}

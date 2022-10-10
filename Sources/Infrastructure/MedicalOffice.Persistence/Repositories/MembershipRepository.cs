using EllipticCurve.Utils;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Dtos.DrugIntractionD;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MembershipRepository : GenericRepository<Membership, Guid>, IMembershipRepository
{
    private readonly IMembershipServiceRepository _repository;
    private readonly IGenericRepository<Membership, Guid> _repositorymembership;
    private readonly ApplicationDbContext _dbContext;
    public List<MembershipListDTO> ListmembershipDTO = null;
    string nameservice;

    public MembershipRepository(IGenericRepository<Membership, Guid> repositorymembership, IMembershipServiceRepository repository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _repositorymembership = repositorymembership;
        _repository = repository;
        _dbContext = dbContext;
        ListmembershipDTO = new List<MembershipListDTO>();
        nameservice = "";
    }

    public async Task<MemberShipService> InsertMembershipIdofServiceAsync(Guid serviceId, Guid membershipId)
    {
        MemberShipService membershipservice = new MemberShipService();
        membershipservice.ServiceId = serviceId;
        membershipservice.MembershipId = membershipId;
        if (membershipservice == null)
            throw new Exception();
        _repository.Add(membershipservice);
        return membershipservice;
    }
    public async Task DeleteMembershipIdofServiceAsync(Guid membershipId)
    {
        var membership = await _dbContext.MemberShipServices.Where(ur => ur.MembershipId == membershipId).ToListAsync();
        if (membership == null)
            throw new Exception();
        foreach (var item in membership)
        {
            _repository.Delete(item);
        }
    }
    public async Task UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId)
    {
        var membership = await _dbContext.MemberShipServices.Where(ur => ur.MembershipId == membershipId).ToListAsync();
        if (membership == null)
            throw new Exception();
        foreach (var item in membership)
        {
            item.ServiceId = serviceId;
            _repository.Update(item);
        }
    }
    public async Task<string> SearchServicesforMemberShip(Guid memid)
    {
        var membershipservice = await _dbContext.MemberShipServices.Where(ms => ms.MembershipId == memid).ToListAsync();
        if (membershipservice == null)
            throw new Exception();
        foreach (var item in membershipservice)
        {
            nameservice += _dbContext.Services.Where(ser => ser.Id == item.ServiceId).FirstOrDefault().Name + "، ".ToString();
        }
        return nameservice;
    }
    public async Task<List<MembershipListDTO>> GetMembership()
    {
        var membership = await _dbContext.Memberships.ToListAsync();
        if (membership == null)
            throw new Exception();
        foreach (var item in membership)
        {
            var membershipservice = await _dbContext.MemberShipServices.Where(ms => ms.MembershipId == item.Id).ToListAsync();
            foreach (var item1 in membershipservice)
            {
                nameservice += _dbContext.Services.Where(ser => ser.Id == item1.ServiceId).SingleOrDefault().Name + " ، ";
            }
            var q = new MembershipListDTO()
            {
                 
                Name = item.Name,
                NameServices = nameservice
            };
            ListmembershipDTO.Add(q);
            nameservice = "";
        }
        return ListmembershipDTO;
    }
}

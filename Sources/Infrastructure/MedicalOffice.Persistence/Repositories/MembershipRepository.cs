﻿using EllipticCurve.Utils;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MembershipRepository : GenericRepository<Membership, Guid>, IMembershipRepository
{
    private readonly ApplicationDbContext _dbContext;
    public List<MembershipListDTO> ListmembershipDTO = null;
    string nameService;

    public MembershipRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        ListmembershipDTO = new List<MembershipListDTO>();
        nameService = "";
    }

    public async Task<MemberShipService> InsertMembershipIdofServiceAsync(Guid serviceId, Guid memberShipId)
    {
        MemberShipService memberShipService = new MemberShipService()
        {
        ServiceId = serviceId,
        MembershipId = memberShipId
        };

        if (memberShipService == null)
            throw new Exception();

        await _dbContext.MemberShipServices.AddAsync(memberShipService);
        return memberShipService;
    }
    public async Task DeleteMembershipIdofServiceAsync(Guid memberShipId)
    {
        var memberShip = await _dbContext.MemberShipServices.Where(ur => ur.MembershipId == memberShipId).ToListAsync();
        if (memberShip == null)
            throw new Exception();
        foreach (var item in memberShip)
        {
            _dbContext.MemberShipServices.Remove(item);
        }
    }
    public async Task UpdateMembershipIdofServiceAsync(Guid serviceId, Guid memberShipId)
    {
        var memberShip = await _dbContext.MemberShipServices.Where(ur => ur.MembershipId == memberShipId).ToListAsync();

        if (memberShip == null)
            throw new Exception();

        foreach (var item in memberShip)
        {
            item.ServiceId = serviceId;
            _dbContext.MemberShipServices.Update(item);
        }
    }
    public async Task<string> SearchServicesforMemberShip(Guid memid)
    {
        var memberShipService = await _dbContext.MemberShipServices.Where(ms => ms.MembershipId == memid).ToListAsync();

        if (memberShipService == null)
            throw new Exception();
        foreach (var item in memberShipService)
        {
            var serviceName = _dbContext.Services.Where(ser => ser.Id == item.ServiceId).FirstOrDefault();

            if (serviceName == null)
                throw new NullReferenceException("Service not Found");

            nameService += serviceName.Name + "، ".ToString();
        }

        return nameService;
    }
    public async Task<List<MembershipListDTO>> GetMembership()
    {
        var memberShip = await _dbContext.Memberships.ToListAsync();

        if (memberShip == null)
            throw new NullReferenceException("MemberShip not Found");

        foreach (var item in memberShip)
        {
            var memberShipService = await _dbContext.MemberShipServices.Where(ms => ms.MembershipId == item.Id).ToListAsync();

            foreach (var item1 in memberShipService)
            {
                var serviceName = _dbContext.Services.Where(ser => ser.Id == item1.ServiceId).FirstOrDefault();

                if (serviceName == null)
                    throw new NullReferenceException("Service not Found");

                nameService += serviceName.Name + " ، ";
            }

            var q = new MembershipListDTO()
            {
                Id = item.Id, 
                Name = item.Name,
                NameServices = nameService,
            };

            ListmembershipDTO.Add(q);
            nameService = "";
        }

        return ListmembershipDTO;
    }
}

﻿using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#nullable disable

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRepository : GenericRepository<MedicalStaff, Guid>, IMedicalStaffRepository
{
    private readonly IUserOfficeRoleRepository _UserOfficeRoleRepository;
    private readonly ApplicationDbContext _dbContext;
    
    string roleName = "";

    public MedicalStaffRepository(IUserOfficeRoleRepository UserOfficeRoleRepository, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _UserOfficeRoleRepository = UserOfficeRoleRepository;
    }

    public async Task UpdateUserOfficeRoleAsync(Guid roleId, Guid MedicalStaffId)
    {
        var MedicalStaff = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == MedicalStaffId).ToListAsync();

        if (MedicalStaff == null)
            throw new Exception();

        foreach (var item in MedicalStaff)
        {
            item.RoleId = roleId;
            _dbContext.UserOfficeRoles.Add(item);
        }

    }

    public async Task DeleteUserOfficeRoleAsync(Guid MedicalStaffId)
    {
        var MedicalStaff = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == MedicalStaffId).ToListAsync();

        if (MedicalStaff == null)
            throw new Exception();

        foreach (var item in MedicalStaff)
        {
            _dbContext.UserOfficeRoles.Remove(item);
        }

    }

    public async Task<List<MedicalStaffListDTO>> GetAllMedicalStaffs(Guid officeID)
    {
        List<MedicalStaffListDTO> medicalStaffListDTOs = new List<MedicalStaffListDTO>();
        var _list = await _dbContext.MedicalStaffs.Where(p => p.OfficeId == officeID).ToListAsync();

        foreach (var item in _list)
        {
            var _userOfficeRoleids = await _dbContext.UserOfficeRoles.Where(p => p.UserId == item.UserId && p.UserId != null).Include(x => x.Role).Where(x => !(x.Role.PersianName.Contains("بیمار") || x.Role.PersianName.Contains("سوپر ادمین"))).ToListAsync();
            Guid[] RoleIds = new Guid[_userOfficeRoleids.Count];
            int i = 0;
            foreach (var item2 in _userOfficeRoleids)
            {
                RoleIds.SetValue(item2.RoleId, i);
                i++;
                roleName += _dbContext.Roles.Select(x => new { x.Id, x.PersianName }).Where(x => x.Id == item2.RoleId).FirstOrDefault().PersianName + "، ";
            };
            MedicalStaffListDTO medicalStaffListDTO = new()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MedicalNumber = item.MedicalNumber,
                SpecializationId = item.SpecializationId,
                SpecializationName = _dbContext.Specializations.Select(x => new { x.Id, x.Name }).Where(x => x.Id == item.SpecializationId).FirstOrDefault().Name,
                IHIOPassword = item.IHIOPassword,
                IHIOUserName = item.IHIOUserName,
                Title = item.Title,
                NationalID = item.NationalID,
                RoleNames = roleName,
                RoleIds = RoleIds
            };
            medicalStaffListDTOs.Add(medicalStaffListDTO);
            roleName = "";
        }
        return medicalStaffListDTOs;

        //var _list = await _dbContext.MedicalStaffs.Where(p => p.OfficeId == officeID).Select(p => new MedicalStaffListDTO
        //{
        //    Id = p.Id,
        //    FirstName = p.FirstName,
        //    LastName = p.LastName,
        //    PhoneNumber = p.PhoneNumber, 
        //    MedicalNumber = p.MedicalNumber,
        //    SpecializationId = p.SpecializationId,
        //    SpecializationName = _dbContext.Specializations.Select(x => new { x.Id, x.Name }).Where(x => x.Id == p.SpecializationId).FirstOrDefault().Name,
        //    IHIOPassword = p.IHIOPassword,
        //    IHIOUserName = p.IHIOUserName,
        //    Title = p.Title,
        //    NationalID = p.NationalID,
        //}).ToListAsync();

        //return _list;
    }

    public async Task<IEnumerable<MedicalStaffNameListDTO>> GetAllMedicalStaffsName()
    {
        var _list = await _dbContext.MedicalStaffs.Select(p => new MedicalStaffNameListDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToListAsync();

        return (IEnumerable<MedicalStaffNameListDTO>)_list;
            roleName = "";
    }

    public Task<List<MedicalStaffNamesDTO>> GetAllMedicalStaffsNamesandRoles()
    {
        List<MedicalStaffNamesDTO> medicalStaffNamesDTO = new();

        var _list = _dbContext.MedicalStaffRoles
            .Include(p => p.MedicalStaff).Include(x => x.Role).Where(x => x.Role.ShowInReception == true);

        foreach (var item in _list)
        {
            MedicalStaffNamesDTO medicalStaffNames = new()
            {
                Id = item.Id,
                FirstName = item.MedicalStaff.FirstName,
                LastName = item.MedicalStaff.LastName,
                RoleId = item.Role.Id,
                RoleName = item.Role.Name,
            };
            medicalStaffNamesDTO.Add(medicalStaffNames);
        }
        return Task.FromResult(medicalStaffNamesDTO);
    }

    public async Task<bool> CheckExistByOfficeIdAndPhoneNumber(Guid officeId, string phoneNumber)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.PhoneNumber == phoneNumber);
        return isExist;
    }

    public async Task<bool> CheckMedicalStaffExist(Guid MedicalStaffId, Guid officeId)
    {
        bool isExist = await _dbContext.MedicalStaffs.AnyAsync(p => p.OfficeId == officeId && p.Id == MedicalStaffId);
        return isExist;
    }
    public async Task<List<MedicalStaff>> GetMedicalStaffBySearch(string name)
    {
        var medicalStaffs = await _dbContext.MedicalStaffs.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name)).ToListAsync();
        return medicalStaffs;
    }
}